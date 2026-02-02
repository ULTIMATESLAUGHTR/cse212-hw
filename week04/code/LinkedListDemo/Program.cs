using System;
using System.Collections.Generic;

/// <summary>
/// Demonstrates linked list operations: Insert, Remove, and Traversal.
/// This learning activity shows how to manipulate a doubly-linked list structure.
/// </summary>
public class LinkedListDemo
{
    public static void Main()
    {
        Console.WriteLine("=== LINKED LIST LEARNING ACTIVITY ===\n");

        // Step 1: Create a LinkedList and populate it with A, B, C, D
        CharLinkedList linkedList = new CharLinkedList();
        linkedList.InsertHead('A');
        linkedList.InsertHead('B');
        linkedList.InsertHead('C');
        linkedList.InsertHead('D');
        
        Console.WriteLine("Initial list (D -> C -> B -> A):");
        PrintList(linkedList);

        // Step 2: Insert X at the head
        Console.WriteLine("\n--- Operation 1: Insert X at the head ---");
        linkedList.InsertHead('X');
        PrintList(linkedList);
        Console.WriteLine("After inserting X at head: X -> D -> C -> B -> A");

        // Step 3: Insert Y between B and C
        Console.WriteLine("\n--- Operation 2: Insert Y between B and C ---");
        linkedList.InsertAfter('B', 'Y');
        PrintList(linkedList);
        Console.WriteLine("After inserting Y after B: X -> D -> C -> Y -> B -> A");

        // Step 4: Remove D (the tail)
        Console.WriteLine("\n--- Operation 3: Remove D (the tail) ---");
        linkedList.Remove('D');
        PrintList(linkedList);
        Console.WriteLine("After removing D: X -> C -> Y -> B -> A");

        // Step 5: Remove B
        Console.WriteLine("\n--- Operation 4: Remove B ---");
        linkedList.Remove('B');
        PrintList(linkedList);
        Console.WriteLine("After removing B: X -> C -> Y -> A");

        Console.WriteLine("\n=== LEARNING ACTIVITY COMPLETE ===");
    }

    /// <summary>
    /// Helper method to print the linked list contents
    /// </summary>
    private static void PrintList(CharLinkedList list)
    {
        Console.Write("List contents: ");
        foreach (char value in list)
        {
            Console.Write(value + " -> ");
        }
        Console.WriteLine("(end)");
    }
}