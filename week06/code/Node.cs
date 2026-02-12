public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problem 1: Insert a value into the binary search tree, and so we changed the method signature to void. 
        // To Fix the duplication issues, I also added a check to prevent inserting duplicate values into the tree.

        // Avoids inserting duplicate values by putting it at the beginning of the method so we catch it before it happens.
        if (value == Data)
            return;

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2 - By setting up the Base case like this first, I can avoid unnecessary recursive calls. 
        // This will also improve the efficiency of the search performance to be O(log n) in a balanced tree.
        
        // Base case: if value matches current node then return true.
        if (value == Data)
            return true;

        // If value is less than current node, search left
        if (value < Data)
        {
            if (Left is null)
                return false;
            else
                return Left.Contains(value);
        }
        else
        {
            // If value is greater than current node, search right
            if (Right is null)
                return false;
            else
                return Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        // Get the height of left subtree (0 if null)
        int leftHeight = Left?.GetHeight() ?? 0;
        // Get the height of right subtree (0 if null)
        int rightHeight = Right?.GetHeight() ?? 0;
        // Return 1 plus the maximum height of either subtree
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}