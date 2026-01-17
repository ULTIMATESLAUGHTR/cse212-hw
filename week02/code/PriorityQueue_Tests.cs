using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities (5, 10, 8) and dequeue to verify highest priority is removed first
    // Expected Result: First dequeue returns the item with priority 10, then 8, then 5
    // Defect(s) Found: The Dequeue() method has two bugs: (1) The loop condition "index < _queue.Count - 1" skips checking the last item in the queue, missing the highest priority if it's in the last position. (2) After finding the highest priority item, the code returns its value but never removes it from the queue using _queue.RemoveAt(), so the queue never gets smaller and the same item could be dequeued multiple times.
    // How it was solved: I traced through the expected behavior manually: when we dequeue, we need to find the highest priority AND remove it. The loop must check ALL items including the last one (index < _queue.Count), and after finding the highest priority index, we must call _queue.RemoveAt(highPriorityIndex) before returning the value.
    // Here are the example of the tests to run:
    // Enqueue: ("A", 5), ("B", 10), ("C", 8)
    // Dequeue: returns "B" (priority 10)
    // Dequeue: returns "C" (priority 8)
    // Dequeue: returns "A" (priority 5)
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 10);
        priorityQueue.Enqueue("C", 8);

        Assert.AreEqual("B", priorityQueue.Dequeue()); // Should return B (priority 10)
        Assert.AreEqual("C", priorityQueue.Dequeue()); // Should return C (priority 8)
        Assert.AreEqual("A", priorityQueue.Dequeue()); // Should return A (priority 5)
    }

    [TestMethod]
    // Scenario: Add multiple items with the same highest priority to test FIFO ordering when priorities are equal
    // Expected Result: When multiple items have the same priority, the first one added (closest to front) should be removed first
    // Defect(s) Found: Same as Test 1 - the loop skips the last item and the item is never removed from the queue
    // How it was solved: Fixed the loop boundary and added the RemoveAt call. Additionally, the comparison must use >= (greater than or equal) to maintain FIFO order when priorities are equal. When we find an item with priority >= current highest, we update to that index, which ensures we get the first occurrence of the highest priority (not the last).
    // Here are the example of the tests ran to solve this problem:
    // Enqueue: ("First", 5), ("Second", 5), ("Third", 5)
    // Dequeue: returns "First"
    // Dequeue: returns "Second"
    // Dequeue: returns "Third"
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 5);

        Assert.AreEqual("First", priorityQueue.Dequeue());  // FIFO: First should come out first
        Assert.AreEqual("Second", priorityQueue.Dequeue()); // Then Second
        Assert.AreEqual("Third", priorityQueue.Dequeue());  // Then Third
    }

    [TestMethod]
    // Scenario: Test that the highest priority item is correctly identified and removed even when it's at the end of the queue
    // Expected Result: Dequeue should return the item with highest priority (10) even though it was added last
    // Defect(s) Found: The loop condition "index < _queue.Count - 1" causes the last item to never be checked, so if the highest priority item is at the end, it won't be found
    // How it was solved: Changed loop condition to "index < _queue.Count" so all items including the last one are evaluated when searching for the highest priority
    // Here are the example of the tests ran to solve this problem:
    // Enqueue: ("Low", 3), ("Medium", 7), ("Highest", 10)
    // Dequeue: returns "Highest"
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 3);
        priorityQueue.Enqueue("Medium", 7);
        priorityQueue.Enqueue("Highest", 10);  // Added last but has highest priority

        Assert.AreEqual("Highest", priorityQueue.Dequeue()); // Should still find and return the highest priority item
    }

    [TestMethod]
    // Scenario: Test that dequeue actually removes the item from the queue so the queue size decreases
    // Expected Result: After each dequeue, the queue should have one fewer item, and eventually should be empty
    // Defect(s) Found: The Dequeue method never calls _queue.RemoveAt(highPriorityIndex), so items are never removed from the queue
    // How it was solved: Added the line "_queue.RemoveAt(highPriorityIndex);" right before returning the value, so the item is removed from the queue
    // Here are the example of the tests ran to solve this problem:
    // Enqueue: ("A", 5), ("B", 10), ("C", 3)
    // Dequeue: removes one item
    // Dequeue: removes one item
    // Dequeue: removes one item
    // Dequeue: throws InvalidOperationException (queue is empty)
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 10);
        priorityQueue.Enqueue("C", 3);

        // Check queue state by counting dequeues
        priorityQueue.Dequeue();
        priorityQueue.Dequeue();
        priorityQueue.Dequeue();

        // Try to dequeue from empty queue - should throw exception
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown InvalidOperationException");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Test that dequeueing from an empty queue throws the correct exception with the correct message
    // Expected Result: InvalidOperationException should be thrown with message "The queue is empty."
    // Defect(s) Found: The empty queue check exists and works correctly in the current code
    // How it was solved: The code already has the correct check: "if (_queue.Count == 0) throw new InvalidOperationException("The queue is empty.");", so no fix was needed for this requirement
    // Here are the example of the tests ran to solve this problem:
    // Dequeue on empty queue: throws InvalidOperationException ("The queue is empty.")
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown InvalidOperationException");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}