public static class DisplaySums {
    public static void Run() {
        DisplaySumPairs([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
        // Should show something like (order does not matter):
        // 6 4
        // 7 3
        // 8 2
        // 9 1 

        Console.WriteLine("------------");
        DisplaySumPairs([-20, -15, -10, -5, 0, 5, 10, 15, 20]);
        // Should show something like (order does not matter):
        // 10 0
        // 15 -5
        // 20 -10

        Console.WriteLine("------------");
        DisplaySumPairs([5, 11, 2, -4, 6, 8, -1]);
        // Should show something like (order does not matter):
        // 8 2
        // -1 11
    }

    /// <summary>
    /// Display pairs of numbers (no duplicates should be displayed) that sum to
    /// 10 using a set in O(n) time.  We are assuming that there are no duplicates
    /// in the list.
    /// </summary>
    /// <param name="numbers">array of integers</param>
    private static void DisplaySumPairs(int[] numbers) {
        // How can you solve the problem using a set data structure?
        // A HashSet gives us O(1) lookup time, which is the key to solving this in O(n) time instead of O(n^2).
        // The insight is: for each number we see, we can instantly calculate what the "complement" number 
        // would need to be to sum to 10 (that's 10 minus the current number). Then we use the set's fast 
        // Contains() method to check if we've already seen that complement number earlier in the list.
        // If we have seen it before, we've found a pair. We print it out, then add the current number to the set 
        // for future comparisons. By looping through the array only once and using O(1) lookups in the set,
        // the total performance is O(n). This is much faster than nested loops that would check every pair 
        // combination, which would be O(n^2). The set remembers everything we've already processed, so we 
        // avoid duplicates naturally - we only print a pair when we encounter the second number of that pair.
        var valuesSeen = new HashSet<int>();
        foreach (var n in numbers) {
            // If 10-n is in the valuesSeen set then we know we have previously 
            // seen a number that will sum with n to equal 10. Print out that pair.
            if (valuesSeen.Contains(10 - n))
                Console.WriteLine($"{n} {10 - n}");
            // Add this number to the valuesSeen set
            valuesSeen.Add(n);
        }
    }
    
    // Results from DisplaySumsSolution:
    // Test 1 [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]:
    //   6 4, 7 3, 8 2, 9 1 - correctly found all pairs that sum to 10
    // Test 2 [-20, -15, -10, -5, 0, 5, 10, 15, 20]:
    //   10 0, 15 -5, 20 -10 - works with negative numbers and zero as complements
    // Test 3 [5, 11, 2, -4, 6, 8, -1]:
    //   8 2, -1 11 - correctly handles mixed positive/negative numbers
    // The algorithm efficiently found all unique pairs with no duplicates by 
    // only printing each pair once when the complement is encountered.
}