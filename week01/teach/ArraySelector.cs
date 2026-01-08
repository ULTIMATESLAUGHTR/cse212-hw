public static class ArraySelector
{
    public static void Run()
    {
        var l1 = new[] { 1, 2, 3, 4, 5 };
        var l2 = new[] { 2, 4, 6, 8, 10};
        var select = new[] { 1, 1, 1, 2, 2, 1, 2, 2, 2, 1};
        var intResult = ListSelector(l1, l2, select);
        Console.WriteLine("<int[]>{" + string.Join(", ", intResult) + "}"); // <int[]>{1, 2, 3, 2, 4, 4, 6, 8, 10, 5}
    }

    private static int[] ListSelector(int[] list1, int[] list2, int[] select)
    {
        // My Solution Plan -> Create a result array with size equal to selector array.
        // Step 1: Use the pointers/indices for each of the two input arrays.
        // Step 2: Loop through the selector array, and based on each value (1 or 2),
        // Step 3: Add the next element from the corresponding array and advance its pointer.
        
        int[] result = new int[select.Length];
        int index1 = 0; // Pointer for list1
        int index2 = 0; // Pointer for list2
        
        for (int i = 0; i < select.Length; i++)
        {
            if (select[i] == 1)
            {
                result[i] = list1[index1];
                index1++;
            }
            else if (select[i] == 2)
            {
                result[i] = list2[index2];
                index2++;
            }
        }
        
        return result;
    }
}