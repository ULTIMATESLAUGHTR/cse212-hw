public static class UniqueLetters {
    public static void Run() {
        var test1 = "abcdefghjiklmnopqrstuvwxyz"; // Expect True because all letters unique
        Console.WriteLine(AreUniqueLetters(test1));

        var test2 = "abcdefghjiklanopqrstuvwxyz"; // Expect False because 'a' is repeated
        Console.WriteLine(AreUniqueLetters(test2));

        var test3 = "";
        Console.WriteLine(AreUniqueLetters(test3)); // Expect True because its an empty string
    }

    /// <summary>Determine if there are any duplicate letters in the text provided</summary>
    /// <param name="text">Text to check for duplicate letters</param>
    /// <returns>true if all letters are unique, otherwise false</returns>
    private static bool AreUniqueLetters(string text) {
        // TODO Problem 1 - Replace the O(n^2) algorithm to use sets and O(n) efficiency
        // How can we write this with O(n) performance using a set?
        // A HashSet is a data structure that stores unique items and has O(1) average performance for both 
        // checking if an item exists (Contains) and adding new items (Add). Unlike our nested loops which 
        // compare each letter to every other letter (O(n^2)), we can iterate through the string once and 
        // track which letters we've already seen. As we loop through each letter, we first check if it's 
        // already in the set (fast operation). If it is, we found a duplicate and return false. If it's not,
        // we add it to the set. Since we only loop through the string once and each lookup/add is constant 
        // time, the overall performance becomes O(n). This is much better than comparing every letter to 
        // every other letter which will solve the problem efficiently even for longer strings.
        for (var i = 0; i < text.Length; ++i) {
            for (var j = 0; j < text.Length; ++j) {
                // Don't want to compare to yourself ... that will always result in a match
                if (i != j && text[i] == text[j])
                    return false;
            }
        }

        return true;
    }
}