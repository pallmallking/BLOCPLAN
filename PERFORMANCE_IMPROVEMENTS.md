# Performance Improvements

This document details the performance optimizations made to the BLOCPLAN codebase.

## Summary of Changes

### 1. LayoutCalculator.vb - Reduced Redundant Calculations

**Problem**: The `CalculateLayout` method called `areas.Sum()` three times (lines 18-20), recalculating the same value unnecessarily.

**Solution**: Cache the sum in a variable `areaSum` and reuse it.

```vb
' Before:
Dim calib As Single = 540 / Math.Sqrt(areas.Sum())
Dim myWidth As Single = Math.Sqrt((ratioX / ratioY) * areas.Sum())
Dim myHeight As Single = Math.Sqrt((ratioY / ratioX) * areas.Sum())

' After:
Dim areaSum As Single = areas.Sum()
Dim calib As Single = 540 / Math.Sqrt(areaSum)
Dim myWidth As Single = Math.Sqrt((ratioX / ratioY) * areaSum)
Dim myHeight As Single = Math.Sqrt((ratioY / ratioX) * areaSum)
```

**Impact**: Eliminates 2 unnecessary iterations through the areas list.

### 2. LayoutCalculator.vb - Optimized Width Normalization

**Problem**: Three separate division operations using `myWidth` as divisor.

**Solution**: Calculate the multiplicative inverse once and use multiplication instead of division (multiplication is faster than division).

```vb
' Before:
wid(0) = Math.Round(wid(0) / myWidth, 3)
wid(1) = Math.Round(wid(1) / myWidth, 3)
wid(2) = Math.Round(wid(2) / myWidth, 3)

' After:
Dim myWidthInv As Single = 1.0F / myWidth
wid(0) = Math.Round(wid(0) * myWidthInv, 3)
wid(1) = Math.Round(wid(1) * myWidthInv, 3)
wid(2) = Math.Round(wid(2) * myWidthInv, 3)
```

**Impact**: Reduces floating-point divisions from 3 to 1.

### 3. LayoutCalculator.vb - Consolidated Loop Structure

**Problem**: Three separate loops (lines 48-62) iterating over department indices with identical logic.

**Solution**: Combined into a single loop using arrays for widths and counts.

```vb
' Before: 3 separate loops
For k = 0 To c1 - 1
    len(departmentIndices(index)) = areas(departmentIndices(index)) / wid(0)
    wi(departmentIndices(index)) = wid(0)
    index += 1
Next
' ... repeated for c2 and c3

' After: Single consolidated loop
Dim widthArray() As Single = {wid(0), wid(1), wid(2)}
Dim counts() As Integer = {c1, c2, c3}

For widthIdx = 0 To 2
    Dim currentWidth As Single = widthArray(widthIdx)
    For k = 0 To counts(widthIdx) - 1
        Dim deptIdx As Integer = departmentIndices(index)
        len(deptIdx) = areas(deptIdx) / currentWidth
        wi(deptIdx) = currentWidth
        index += 1
    Next
Next
```

**Impact**: Improved code maintainability and reduced code duplication.

### 4. LayoutCalculator.vb - Eliminated List Reversals

**Problem**: Called `Sort()` followed by `Reverse()` on lists, which is inefficient (lines 123-132).

**Solution**: Instead of reversing, access elements in reverse order during iteration using index arithmetic.

```vb
' Before:
distList.Sort()
relList.Sort()
relList.Reverse()  ' Expensive O(n) operation

lBound = 0
For i = 0 To relList.Count - 1
    lBound += relList(i) * distList(i)
Next

distList.Reverse()  ' Another expensive O(n) operation
uBound = 0
For i = 0 To relList.Count - 1
    uBound += relList(i) * distList(i)
Next

' After:
distList.Sort()
relList.Sort()

' Access in reverse without actual reversal
lBound = 0
For i = 0 To relList.Count - 1
    lBound += relList(relList.Count - 1 - i) * distList(i)
Next

uBound = 0
For i = 0 To relList.Count - 1
    uBound += relList(relList.Count - 1 - i) * distList(distList.Count - 1 - i)
Next
```

**Impact**: Eliminates two O(n) list reversal operations.

### 5. LayoutCalculator.vb - Pre-allocated Lists with Capacity

**Problem**: Lists created without initial capacity, causing multiple internal array reallocations as items are added.

**Solution**: Pre-calculate the required capacity and allocate upfront.

```vb
' Before:
Dim distList As New List(Of Single)()
Dim relList As New List(Of Single)()

' After:
Dim totalPairs As Integer = (depNo * (depNo - 1)) \ 2
Dim distList As New List(Of Single)(totalPairs)
Dim relList As New List(Of Single)(totalPairs)
```

**Impact**: Reduces memory allocations and array copying during list growth.

### 6. layoutForm.vb - Cached Reusable Collections

**Problem**: `count_layout()` method created new List and array objects on every call, causing unnecessary garbage collection pressure.

**Solution**: Cache collections as class-level fields and reuse them by clearing and repopulating.

```vb
' Added class-level fields:
Private cachedAreaList As List(Of Single)
Private cachedDepartmentIndices As List(Of Integer)
Private cachedScoreArray As Single(,)

' In count_layout():
If cachedAreaList Is Nothing Then
    cachedAreaList = New List(Of Single)(depNo)
    cachedDepartmentIndices = New List(Of Integer)(depNo)
    cachedScoreArray = New Single(depNo, depNo) {}
End If

cachedAreaList.Clear()
' ... populate and reuse
```

**Impact**: Dramatically reduces allocations when layout is recalculated frequently (e.g., during auto-search).

### 7. layoutForm.vb - Reused Random Instance

**Problem**: Created new `Random` object inside loop in `change_randomDeptId()`, which:
- Is inefficient (object creation overhead)
- Can produce poor randomness when called rapidly (time-based seed)

**Solution**: Use a shared Random instance.

```vb
' Added class-level field:
Private sharedRandom As New Random()

' In method:
' Before:
Dim rndId As New Random

' After:
' Use sharedRandom directly
```

**Impact**: Better randomness and eliminated object creation overhead in loops.

### 8. blocplanForm.vb - Cached Area Texts List

**Problem**: Created new List on every `area_textchanged` event (triggered frequently as user enters data).

**Solution**: Reuse a cached list.

```vb
' Added class-level field:
Private cachedAreaTexts As New List(Of String)(18)

' In method:
' Before:
Dim areaTexts As New List(Of String)

' After:
cachedAreaTexts.Clear()
```

**Impact**: Reduces allocation pressure during data entry.

## Expected Performance Benefits

1. **Reduced CPU Usage**: 
   - Fewer redundant calculations
   - Fewer division operations
   - Eliminated unnecessary list reversals

2. **Reduced Memory Allocations**:
   - Reused collections instead of creating new ones
   - Pre-allocated lists with proper capacity
   - Reduced garbage collection pressure

3. **Improved Responsiveness**:
   - Especially noticeable during:
     - Auto-search operations (500 iterations)
     - Rapid department swapping
     - Data entry in forms

4. **Better Randomness**:
   - Shared Random instance produces better quality random numbers

## Testing Recommendations

To validate these improvements:

1. Run existing unit tests to ensure correctness
2. Benchmark auto-search performance (500 iterations) before and after
3. Monitor memory usage during extended sessions
4. Verify UI responsiveness during data entry

## Backward Compatibility

All changes maintain the same external API and behavior. The optimizations are purely internal implementation improvements that do not affect:
- Method signatures
- Return values
- Side effects
- User-visible behavior
