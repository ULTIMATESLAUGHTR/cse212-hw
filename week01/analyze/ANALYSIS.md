# SearchSorted Performance Analysis
//This is Just so I can understand what the Analysis should look like when described. 
## Function Analysis

### SearchSorted1 - Linear Search
```csharp
private static int SearchSorted1(int[] data, int target) {
    var count = 0;
    foreach (var item in data) {
        count += 1;
        if (item == target)
            return count; // Found it
    }
    return count; // Didn't find it
}
```

**Algorithm:** This function performs a linear search, iterating through each element in the array sequentially until it finds the target or reaches the end.

**Predicted Performance:** O(n)
- The function iterates through every element in the worst case (when the target is not in the list)
- Each iteration does constant O(1) work

---

### SearchSorted2 - Binary Search
```csharp
private static int SearchSorted2(int[] data, int target, int start, int end) {
    if (end < start)
        return 1; // All done
    var middle = (end + start) / 2;
    if (data[middle] == target)
        return 1; // Found it
    if (data[middle] < target) // Search in the upper half after index middle
        return 1 + SearchSorted2(data, target, middle + 1, end);
    // Search in the lower half before index middle
    return 1 + SearchSorted2(data, target, start, middle - 1);
}
```

**Algorithm:** This function performs a binary search, dividing the search space in half with each recursive call.

**Predicted Performance:** O(log n)
- The search space is halved with each recursive call
- Maximum recursion depth is log₂(n)
- Each level of recursion does constant O(1) work

---

## Performance Test Results

| n | sort1-count | sort2-count | sort1-time (ms) | sort2-time (ms) |
|---|---|---|---|---|
| 0 | 0 | 1 | 0.00039 | 0.00033 |
| 1000 | 1000 | 11 | 0.00119 | 0.00006 |
| 2000 | 2000 | 12 | 0.00297 | 0.00008 |
| 3000 | 3000 | 13 | 0.00353 | 0.00006 |
| 4000 | 4000 | 13 | 0.00471 | 0.00006 |
| 5000 | 5000 | 14 | 0.00588 | 0.00007 |
| 10000 | 10000 | 15 | 0.01176 | 0.00008 |
| 15000 | 15000 | 15 | 0.01784 | 0.00009 |
| 20000 | 20000 | 16 | 0.02636 | 0.00008 |
| 25000 | 25000 | 16 | 0.02980 | 0.00008 |

---

## Analysis of Results

### SearchSorted1 (Linear Search) - Actual Performance: **O(n)**

**Loop Iteration Count:**
- For n = 1000: 1000 iterations
- For n = 5000: 5000 iterations
- For n = 25000: 25000 iterations
- The count grows **linearly** with input size, confirming **O(n)** behavior

**Explanation:** Since the test searches for a value not in the list (worst case), the function must iterate through every single element before returning.

### SearchSorted2 (Binary Search) - Actual Performance: **O(log n)**

**Loop Iteration Count (Recursive Calls):**
- For n = 1000: 11 iterations (log₂(1000) ≈ 10)
- For n = 5000: 14 iterations (log₂(5000) ≈ 12.3)
- For n = 25000: 16 iterations (log₂(25000) ≈ 14.6)
- The count grows **logarithmically** with input size, confirming **O(log n)** behavior

**Explanation:** Each recursive call eliminates half the search space, resulting in at most log₂(n) calls to find or determine the absence of the target.

---

## Answer to Questions

### 1. What is the performance using big O notation for each function?

**SearchSorted1 (Linear Search):**
- **Predicted:** O(n)
- **Actual (based on results):** O(n) ✓ Confirmed
- The count value equals n in every test case

**SearchSorted2 (Binary Search):**
- **Predicted:** O(log n)
- **Actual (based on results):** O(log n) ✓ Confirmed
- The count value grows logarithmically (approximately log₂(n))

---

### 2. Which function has the better performance in the worst case?

**SearchSorted2 (Binary Search) has significantly better worst-case performance.**

**Evidence:**

| Metric | SearchSorted1 | SearchSorted2 | Winner |
|---|---|---|---|
| **Big O** | O(n) | O(log n) | SearchSorted2 |
| **Work (n=25000)** | 25000 iterations | 16 iterations | SearchSorted2 |
| **Speed (n=25000)** | 0.02980 ms | 0.00008 ms | SearchSorted2 |
| **Speedup factor** | --- | **372x faster** | SearchSorted2 |

**Why SearchSorted2 is Better:**
1. **Asymptotic complexity:** O(log n) grows much slower than O(n)
2. **Practical performance:** At n=25000, SearchSorted2 performs only 16 iterations vs 25000 for SearchSorted1
3. **Scalability:** The gap widens dramatically as n increases (e.g., at n=1 million, SearchSorted2 would only need ~20 iterations vs 1 million)
4. **Time measurements:** SearchSorted2 consistently uses less than 0.00010 ms while SearchSorted1 grows to 0.02980 ms

---

## Conclusion

Binary search (SearchSorted2) vastly outperforms linear search (SearchSorted1) in the worst case, demonstrating the critical importance of algorithm selection. The O(log n) vs O(n) difference becomes increasingly pronounced as the dataset grows, making binary search the clear choice for searching in sorted arrays.
