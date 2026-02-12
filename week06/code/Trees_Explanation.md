# Trees Explanation

## Introduction to Trees

A tree is a hierarchical data structure that starts with a single root node and branches out to multiple child nodes, forming a tree-like structure. Unlike linear data structures such as arrays or linked lists, trees enable efficient searching, sorting, and organization of data. Tree data structures are fundamental to computer science and are used in file systems, databases, syntax trees in compilers, and many algorithms.

The key characteristic of a tree is that it has no circular loops and all nodes are connected. Each node in a tree can have multiple child nodes, and each node (except the root) has exactly one parent node.

---

## Binary Trees

### What is a Binary Tree?

A binary tree is a specialized tree data structure where each node has at most two children, typically referred to as the **left child** and the **right child**. This constraint makes binary trees easier to implement and reason about compared to trees with unlimited children per node.

### Structure of a Binary Tree Node

Each node in a binary tree contains:
1. **Data/Value**: The information stored in the node
2. **Left Pointer**: Reference to the left child node (or null if no left child)
3. **Right Pointer**: Reference to the right child node (or no right child)

### Binary Tree Example

```
        A
       / \
      B   C
     / \
    D   E
```

In this example:
- A is the root node
- B and C are children of A
- D and E are children of B
- D and E are leaf nodes (no children)

### Node Class Implementation

```csharp
public class Node
{
    public int Value { get; set; }
    public Node? LeftChild { get; set; }
    public Node? RightChild { get; set; }

    public Node(int value)
    {
        Value = value;
        LeftChild = null;
        RightChild = null;
    }
}
```

### Traversing a Binary Tree

Traversing a binary tree means visiting all nodes and their values. There are several common traversal methods:

**Pre-order Traversal** (Root → Left → Right):
```csharp
public void PreOrderTraverse(Node? node)
{
    if (node == null)
        return;

    Console.WriteLine(node.Value);          // Process root
    PreOrderTraverse(node.LeftChild);       // Traverse left
    PreOrderTraverse(node.RightChild);      // Traverse right
}
```

**In-order Traversal** (Left → Root → Right):
```csharp
public void InOrderTraverse(Node? node)
{
    if (node == null)
        return;

    InOrderTraverse(node.LeftChild);        // Traverse left
    Console.WriteLine(node.Value);          // Process root
    InOrderTraverse(node.RightChild);       // Traverse right
}
```

**Post-order Traversal** (Left → Right → Root):
```csharp
public void PostOrderTraverse(Node? node)
{
    if (node == null)
        return;

    PostOrderTraverse(node.LeftChild);      // Traverse left
    PostOrderTraverse(node.RightChild);     // Traverse right
    Console.WriteLine(node.Value);          // Process root
}
```

---

## Binary Search Trees

### What is a Binary Search Tree?

A Binary Search Tree (BST) is a binary tree with a special ordering property: for each node, all values in the left subtree are less than the node's value, and all values in the right subtree are greater than the node's value. This property makes searching, insertion, and deletion very efficient.

### BST Property

For any node in a Binary Search Tree:
- All values in the left subtree < node value
- All values in the right subtree > node value

### Binary Search Tree Example

```
        50
       /  \
      30   70
     / \   / \
    20 40 60 80
```

In this example:
- 50 is the root
- All values left of 50 (20, 30, 40) are less than 50
- All values right of 50 (60, 70, 80) are greater than 50

### Search in a Binary Search Tree

The search operation leverages the BST property to efficiently find values. By using the ordering, we can eliminate half of the remaining nodes with each comparison, resulting in **O(log n)** time complexity for balanced trees.

```csharp
public bool Search(Node? node, int target)
{
    // Base case: node not found
    if (node == null)
        return false;

    // Found the target
    if (node.Value == target)
        return true;

    // Target is less than current node, search left
    if (target < node.Value)
        return Search(node.LeftChild, target);

    // Target is greater than current node, search right
    return Search(node.RightChild, target);
}
```

### Insertion in a Binary Search Tree

When inserting a new value, we find the appropriate position by comparing with each node and moving left or right based on the BST property.

```csharp
public Node? Insert(Node? node, int value)
{
    // Base case: create new node
    if (node == null)
        return new Node(value);

    // Value already exists
    if (node.Value == value)
        return node;

    // Insert into left subtree
    if (value < node.Value)
        node.LeftChild = Insert(node.LeftChild, value);
    else
        // Insert into right subtree
        node.RightChild = Insert(node.RightChild, value);

    return node;
}
```

### Deletion in a Binary Search Tree

Deletion is more complex as we need to handle three cases:
1. Node has no children (leaf)
2. Node has one child
3. Node has two children

```csharp
public Node? Delete(Node? node, int value)
{
    if (node == null)
        return null;

    if (value < node.Value)
    {
        node.LeftChild = Delete(node.LeftChild, value);
    }
    else if (value > node.Value)
    {
        node.RightChild = Delete(node.RightChild, value);
    }
    else
    {
        // Case 1: No children (leaf node)
        if (node.LeftChild == null && node.RightChild == null)
            return null;

        // Case 2: One child
        if (node.LeftChild == null)
            return node.RightChild;
        if (node.RightChild == null)
            return node.LeftChild;

        // Case 3: Two children - find the smallest value in right subtree (in-order successor)
        Node? minNode = FindMin(node.RightChild);
        node.Value = minNode.Value;
        node.RightChild = Delete(node.RightChild, minNode.Value);
    }

    return node;
}

private Node FindMin(Node node)
{
    while (node.LeftChild != null)
        node = node.LeftChild;
    return node;
}
```

### Time Complexity of BST Operations

| Operation | Average Case | Worst Case    |
|-----------|--------------|---------------|
| Search    | O(log n)     | O(n)          |
| Insert    | O(log n)     | O(n)          |
| Delete    | O(log n)     | O(n)          |

The worst case occurs when the tree becomes unbalanced (essentially a linked list).

---

## Balanced Binary Search Trees

### The Problem with Unbalanced Trees

Consider a BST created by inserting values in sorted order: 1, 2, 3, 4, 5

```
    1
     \
      2
       \
        3
         \
          4
           \
            5
```

This tree has completely lost its searching advantage! It behaves like a linked list with **O(n)** time complexity for search operations. This is why balanced trees are so important.

### What is a Balanced Tree?

A tree is balanced when the height of the tree is approximately the same on both left and right sides. More formally, a tree is balanced if the height difference between left and right subtrees is small (usually no more than 1) for all nodes.

### Balanced Tree Example

```
        3
       / \
      2   4
     /     \
    1       5
```

This tree is balanced. The height difference between left and right subtrees at each node is at most 1.

### Advantages of Balanced Trees

A balanced BST guarantees:
- **O(log n)** time complexity for search, insertion, and deletion
- Predictable performance regardless of insertion order
- Efficient operations even for large datasets

### Self-Balancing Trees

There are several algorithms that automatically balance trees:

#### AVL Trees (Adelson-Velskii and Landis)

AVL trees are self-balancing binary search trees that check the balance factor (height difference between left and right subtrees) after each modification. If the tree becomes unbalanced, rotations are performed to restore balance.

**Balance Factor** = Height of Left Subtree - Height of Right Subtree

For an AVL tree to be valid: -1 ≤ Balance Factor ≤ 1

**Rotations** are used to rebalance:
- **Left Rotation**: When right subtree is too tall
- **Right Rotation**: When left subtree is too tall
- **Left-Right Rotation**: Combination for specific unbalanced patterns
- **Right-Left Rotation**: Combination for specific unbalanced patterns

```csharp
// Example: Right rotation when tree becomes unbalanced
//
//      z                y
//     /                / \
//    y        -->     x   z
//   /
//  x

private Node RotateRight(Node node)
{
    Node newRoot = node.LeftChild;
    node.LeftChild = newRoot.RightChild;
    newRoot.RightChild = node;
    return newRoot;
}
```

#### Red-Black Trees #### This is Something Else that can work for thinking about binary Trees by adding a color identifier. ####

Red-Black Trees are another self-balancing binary search tree that uses a color property (red or black) on each node along with specific rules to maintain balance. They provide slightly less strict balancing than AVL trees but require fewer rotations during modifications.

Red-Black Tree Properties:
1. Every node is either red or black
2. The root is always black
3. All leaves (NIL) are black
4. If a node is red, then both its children are black
5. Every path from a node to its leaves has the same number of black nodes

### Performance Guarantee

With a balanced binary search tree, operations are guaranteed **O(log n)** performance:
- Tree height = O(log n)
- Search: Follow one path from root to leaf = O(log n)
- Insert/Delete: Search + rebalancing operations = O(log n)

---

## Practice Problem: BST Operations

Consider building a binary search tree from the following values in this order: 50, 30, 70, 20, 40, 60, 80

### Questions to Consider

1. **Draw the resulting BST.** What does the tree look like after inserting all values in the given order?

2. **Search for values.** Trace through the search algorithm to find 40 and 25. How many comparisons does each search take?

3. **Delete operations.** Delete the root node (50). What value replaces it? Is this still a valid BST?

4. **Balance analysis.** Is the resulting tree balanced? Calculate the height and balance factors for each node.

### Solution Outline

The resulting tree should look like:
```
        50
       /  \
      30   70
     / \   / \
    20 40 60 80
```

- Search for 40: 50 → 30 → 40 (3 comparisons)
- Search for 25: 50 → 30 → 20 → null (not found, 4 comparisons)
- Delete 50: The in-order successor is 60, so 60 becomes the new root
- Balance factor: All nodes have balance factors of 0 or ±1, so this tree is balanced

---

## Key Terms To Remember ##

- **AVL Tree**: Adelson-Velskii and Landis Tree. A balanced binary search tree that is checked for unbalanced height after every modification to the tree. If the tree is unbalanced, then pre-determined algorithms are used to balance the tree.

- **balanced**: A tree is balanced if the height of the tree from the root to each leaf is consistent for all subtrees. The measure of consistency will vary between algorithms but usually does not exceed a height difference of 1.

- **balanced binary search tree**: A binary search tree which is balanced or restructured to be balanced. A balanced binary search tree has O(log n) performance when searching.

- **binary search tree**: A binary tree that puts data less than the root to the left and greater than the root to the right. This type of a tree enables searching algorithms to be efficient.

- **binary tree**: A tree that has up to two children for each node.

- **child**: A child is a node connected from a parent node.

- **leaf**: A leaf is a node that has no children.

- **node**: An entry in a tree that contains both the value and pointers to any children nodes.

- **parent**: A parent is a node that connects to children nodes.

- **Red Black Tree**: A self-balancing binary search tree.

- **root**: The first parent in a tree.

- **subtree**: Subset of a tree made by selecting a node to be the root and including all the children from that node.

- **traverse**: The process of visiting all nodes (and subsequently their values) in a tree. Used frequently with a binary search tree using recursion to start at the leaf node that contains the smallest value and going to the leaf node that contains the largest value.

- **trees**: A data structure that starts with a root node and is subsequently connected to multiple nodes according to a relationship between the nodes. The tree does not have any circular loops or unconnected nodes.
