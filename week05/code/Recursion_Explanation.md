# Recursion Explanation

## How Recursion Works in C#

Recursion is a programming technique where a function calls itself to solve a smaller version of the same problem. Every recursive function must have:

1. **Base Case(s)**: The condition that stops the recursion
2. **Recursive Case**: The function calling itself with a modified parameter, moving toward the base case

Recursion plays an important role in several searching and sorting algorithms. Understanding recursion is fundamental to mastering algorithms that process hierarchical or iterative data structures.

## Binary Search Example

The binary search algorithm assumes that the data is already sorted. Just like a phone book, if you had sorted data, then the best way to find something is to look in the middle of the data set. By looking in the middle of the sorted data, we can quickly exclude half of the data with a single comparison.

### Binary Search Algorithm

**Base Case 1:** If the list has just one item, then check it and return the result.

**Base Case 2:** If the number in the middle of the list is what we are looking for, then the value is in the list.

**Recursive Case:** If the number in the middle of the list is not what we are looking for, then search in either the first half (lower values) or the second half (higher values).

### Implementation Notes

Calling the binary search function recursively on the list subset can be done in two ways:

1. **Creating a new list** - Simpler to understand but uses more memory
2. **Using starting and ending indices** - More memory efficient but slightly more complex

The first approach (used below) is easier to visualize the recursion process.

### Binary Search Code
public bool BinarySearch(int[] sortedArray, int target)
{
    if (sortedArray.Length == 1)
    {
        // Base case: single element left
        return target == sortedArray[0];
    }
    else
    {
        // Find the middle and compare
        var middle = sortedArray.Length / 2;

        if (target == sortedArray[middle])
        {
            // We got lucky and the middle was the match
            return true;
        }
        else if (target < sortedArray[middle])
        {
            // Search the first half (index 0 to middle-1) and return the result
            return BinarySearch(sortedArray[..middle], target);
        }
        else
        {
            // Search the second half (index middle to end) and return the result
            return BinarySearch(sortedArray[middle..], target);
        }
    }
}
```

### Usage Examples
Console.WriteLine(BinarySearch(new[]{1, 3, 6, 18, 20, 25, 34, 38, 89, 95, 99, 100}, 89)); // true
Console.WriteLine(BinarySearch(new[]{1, 3, 6, 18, 20, 25, 34, 38, 89, 95, 99, 100}, 1));  // true
Console.WriteLine(BinarySearch(new[]{1, 3, 6, 18, 20, 25, 34, 38, 89, 95, 99, 100}, 17)); // false
```

### Time Complexity

The performance of this recursive algorithm is **O(log n)** because we are excluding half of the list with each comparison. This makes binary search extremely efficient even for large datasets.

---

## Practice Problem: Recursive Sum

Consider using a recursive function to sum all of the numbers from 1 to n.

### Questions to Consider

1. **What is the "smaller version" of this problem?** What smaller problem can be used to solve the case of adding some number such as n?

2. **What is the base case?** What is the condition that will stop the recursion?

3. **Write the code for this function.** Implement a recursive function that sums numbers from 1 to n.

### Hints

- Think about how you can express the sum of 1 to n in terms of n plus the sum of 1 to (n-1)
- What would be the simplest case? The base case should handle when n equals what value?
- Remember to always modify the problem toward the base case

### Solution Resolved is - 

public void Sum(int n)
{
    if (n == 1)
    {
        return 1;
    }

    return n + Sum(n - 1) ;
}

## Memoization (This part's very important to reduce function calls)

### What is Memoization?

Memoization is an optimization technique used to improve the performance of recursive functions by storing the results of expensive function calls and returning the cached result when the same inputs occur again. The term "memoization" comes from the word "memo," meaning it helps the function remember previous results.

Memoization is particularly valuable for recursive algorithms that exhibit **overlapping subproblems** — situations where the same calculation is performed multiple times with identical parameters.

### Why Memoization Matters for Recursion

Without memoization, recursive functions can suffer from exponential time complexity due to redundant calculations. By caching results, we can reduce the time complexity significantly, often from exponential to polynomial time. This is crucial for making recursive solutions practical for larger datasets.

### Fibonacci Sequence Example

The Fibonacci sequence is a classic example where memoization dramatically improves performance. Consider this naive recursive implementation:

```csharp
public int Fib(int n)
{
    if (n <= 2)
    {
        // Fib(2) = 1 and Fib(1) = 1
        return 1;
    }
    else
    {
        // Fib(n) = Fib(n - 1) + Fib(n - 2)
        return Fib(n - 1) + Fib(n - 2);
    }
}
```

#### The Problem

This simple recursive function recalculates the same values repeatedly. For example, when computing `Fib(5)`:
- `Fib(5)` calls `Fib(4)` and `Fib(3)`
- `Fib(4)` calls `Fib(3)` and `Fib(2)`
- `Fib(3)` is calculated twice already, and it will be calculated again

The call tree grows exponentially, with many duplicate calculations. The time complexity is **O(2^n)**, which becomes prohibitively slow for even moderate values of n (e.g., n = 40).

#### Memoization Solution

By storing previously calculated results in a cache (typically a dictionary or array), we can avoid redundant calculations:

```csharp
public int FibMemo(int n, Dictionary<int, int> memo = null)
{
    // Initialize the memo cache on first call
    if (memo == null)
    {
        memo = new Dictionary<int, int>();
    }

    // Check if result is already cached
    if (memo.ContainsKey(n))
    {
        return memo[n];
    }

    // Base case
    if (n <= 2)
    {
        return 1;
    }

    // Calculate and store in memo
    memo[n] = FibMemo(n - 1, memo) + FibMemo(n - 2, memo);
    
    return memo[n];
}
```

#### Alternative Approach Using Array

For better performance, you can use an array instead of a dictionary:

```csharp
public int FibMemoArray(int n, int[] memo = null)
{
    // Initialize the memo array on first call
    if (memo == null)
    {
        memo = new int[n + 1];
    }

    // Check if result is already cached
    if (memo[n] != 0)
    {
        return memo[n];
    }

    // Base case
    if (n <= 2)
    {
        return 1;
    }

    // Calculate and store in memo
    memo[n] = FibMemoArray(n - 1, memo) + FibMemoArray(n - 2, memo);
    
    return memo[n];
}
```

### Performance Comparison

With memoization, the time complexity improves from **O(2^n)** to **O(n)**, which is a dramatic improvement:

- Without memoization: `Fib(40)` requires approximately 1 billion function calls
- With memoization: `Fib(40)` requires only 40 function calls

The space complexity for both approaches is O(n) due to the cache storage and recursion call stack.

### Key Takeaways

1. **Memoization reduces redundant calculations** by caching results
2. **Choose appropriate data structures** — dictionaries for sparse data, arrays for dense data
3. **Memoization is best used when** there are overlapping subproblems
4. **Trade-off**: Memory usage increases, but execution time decreases significantly

Memoization is a powerful technique that transforms impractical recursive solutions into efficient ones, making it essential for real-world applications.
