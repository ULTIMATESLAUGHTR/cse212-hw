using System;
using System.Collections.Generic;

/// <summary>
/// Demonstrates linked list operations I need to learn this week - > Insert, Remove, and Traversal.
/// This learning activity shows me how to manipulate a doubly-linked list structure.
/// </summary>
public class LinkedListLearning
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

/// <summary>
/// A modified LinkedList class that works with characters instead of integers.
/// This class demonstrates the core operations needed for the learning activity.
/// </summary>
public class CharLinkedList : System.Collections.IEnumerable
{
    private CharNode? _head;
    private CharNode? _tail;

    /// <summary>
    /// Insert a new node at the front (head) of the linked list.
    /// </summary>
    public void InsertHead(char value)
    {
        CharNode newNode = new(value);
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
        }
    }

    /// <summary>
    /// Insert a new node at the back (tail) of the linked list.
    /// </summary>
    public void InsertTail(char value)
    {
        CharNode newNode = new(value);
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Prev = _tail;
            _tail.Next = newNode;
            _tail = newNode;
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(char value, char newValue)
    {
        CharNode? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                else
                {
                    CharNode newNode = new(newValue);
                    newNode.Prev = curr;
                    newNode.Next = curr.Next;
                    curr.Next!.Prev = newNode;
                    curr.Next = newNode;
                }
                return;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(char value)
    {
        CharNode? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If this is the only node in the list
                if (curr == _head && curr == _tail)
                {
                    _head = null;
                    _tail = null;
                }
                // If this is the head node
                else if (curr == _head)
                {
                    _head = curr.Next;
                    _head!.Prev = null;
                }
                // If this is the tail node
                else if (curr == _tail)
                {
                    _tail = curr.Prev;
                    _tail!.Next = null;
                }
                // If this is a middle node
                else
                {
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;
                }
                return;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Iterate through the Linked List
    /// </summary>
    public System.Collections.IEnumerator GetEnumerator()
    {
        CharNode? curr = _head;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }
}

/// <summary>
/// A node in the character linked list. Each node holds a character value
/// and references to the next and previous nodes.
/// </summary>
public class CharNode
{
    public char Data { get; set; }
    public CharNode? Next { get; set; }
    public CharNode? Prev { get; set; }

    public CharNode(char data)
    {
        this.Data = data;
    }
}
