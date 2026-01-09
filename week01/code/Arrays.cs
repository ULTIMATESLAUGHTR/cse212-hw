public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        //My Step by Step Plan to work out this MultiplesOf function will be - 
        //Step 1: You'll need to create a new array of doubles with the size of 'length'. For example -> double[] multiples = new double[length];
        //Step 2: After setting up the array, use a "for loop" to iterate from 0 to length - 1. Example: for (int i = 0; i < length; i++)
        //Step 3: Inside the loop, calculate the multiple by multiplying number with (i + 1) where i is the current index. For example -> multiple = number * (i + 1);
        //Step 4: Assign the calculated multiple to the array at index i. The basic setup should be - multiples[i] = multiple;
        //Step 5: After the loop completes, return the new populated array. To accomplish the requirement, rename the return - "multiples" matching the variable set up earlier.
        double[] multiples = new double[length];
        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        //My Step by Step Plan to work out this RotateListRight function will be -
        //Step 1: First, determine the length of the list using data.Count and store it in a variable called length. Example: int length = data.Count;
        //Step 2: Next, create a new list to hold the rotated values. Example: List<int> rotated = new List<int>(new int[length]);
        //Step 3: Use a "for loop" to iterate through each index of the original list. Example: for (int i = 0; i < length; i++)
        //Step 4: Inside the loop, calculate the new index for each element after rotation. The new index can be calculated as (i + amount) % length.
        //Step 5: Assign the value from the original list to the new index in the rotated list. Example: rotated[newIndex] = data[i];
        //Step 6: After the loop, copy the values from the rotated list back to the original list to modify it in place. Example: for (int i = 0; i < length; i++) { data[i] = rotated[i]; }
        int length = data.Count;
        List<int> rotated = new List<int>(new int[length]);
        for (int i = 0; i < length; i++)
        {
            int newIndex = (i + amount) % length;
            rotated[newIndex] = data[i];
        }
        for (int i = 0; i < length; i++)
        {
            data[i] = rotated[i];
        }
    }
}
