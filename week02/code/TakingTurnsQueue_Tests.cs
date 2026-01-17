using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 1 - Run test cases and record any defects the test code finds in the comment above the test method.
// DO NOT MODIFY THE CODE IN THE TESTS in this file, just the comments above the tests. 
// Fix the code being tested to match requirements and make all tests pass. 

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3) and
    // run until the queue is empty
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: The GetNextPerson() method uses the condition "if (person.Turns > 1)" to decide whether to re-enqueue a person. This is incorrect because it fails to re-enqueue when person.Turns equals 1 (on their last turn, they should still be dequeued without re-enqueueing). The condition should check if the person still has turns remaining (> 0) before decrementing, and re-enqueue only if they have MORE than 1 turn left OR if they have infinite turns (0 or less).
    // How it was solved: I traced through the expected output manually step-by-step. When I compared the condition "person.Turns > 1" against the expected sequence, I found that when someone had exactly 1 turn left, they weren't being added back to the queue, causing the sequence to terminate early. The fix uses separate conditions: one for infinite turns (Turns <= 0), one for finite turns with more to go (Turns > 1), and implicitly handles the case where Turns == 1 by not re-enqueueing.
    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3)
    // After running 5 times, add George with 3 turns.  Run until the queue is empty.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
    // Defect(s) Found: The GetNextPerson() method uses the condition "if (person.Turns > 1)" which fails to properly re-enqueue people. People with exactly 1 turn remaining are not re-enqueued, which breaks the expected sequence. Additionally, people with infinite turns (0 or negative) are never re-enqueued. The condition should distinguish between finite turns (decrement and re-enqueue if > 0) and infinite turns (never decrement, always re-enqueue).
    // How it was solved: I identified that the logic needed three separate paths: (1) if turns are infinite (0 or less), always re-enqueue without changing the value, (2) if turns are finite but still have more than 1 left, decrement and re-enqueue, (3) if turns equal exactly 1, don't re-enqueue since it's the last turn. This handles both the finite and infinite turn cases properly.
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);
        var george = new Person("George", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, george, sue, tim, george, tim, george];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        for (; i < 5; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        players.AddPerson("George", 3);

        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);

            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (Forever), Sue (3)
    // Run 10 times.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: The GetNextPerson() method does not handle infinite turns (turns = 0). People with infinite turns (0 or negative) are never re-enqueued because the condition "if (person.Turns > 1)" evaluates to false. Tim with 0 turns should be re-enqueued indefinitely without decrementing.
    // How it was solved: I realized the original code never checked for zero or negative turns. The fix adds a first condition that checks "if (person.Turns <= 0)" and immediately re-enqueues the person without modifying the Turns value. This keeps them in the queue forever with their original value unchanged.
    public void TestTakingTurnsQueue_ForeverZero()
    {
        var timTurns = 0;

        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that the people with infinite turns really do have infinite turns.
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
    }

    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Tim (Forever), Sue (3)
    // Run 10 times.
    // Expected Result: Tim, Sue, Tim, Sue, Tim, Sue, Tim, Tim, Tim, Tim
    // Defect(s) Found: The GetNextPerson() method does not handle negative turns (infinite turns). People with negative turns (-3) are never re-enqueued because the condition "if (person.Turns > 1)" evaluates to false. Tim with -3 turns should be re-enqueued indefinitely without modification to the Turns value.
    // How it was solved: Similar to the zero turns case, I added a check for "person.Turns <= 0" which catches both zero AND negative values. This single condition handles all infinite turn cases (whether the person passes 0 or any negative number) by always re-enqueueing them without ever modifying their Turns value.
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        var timTurns = -3;
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [tim, sue, tim, sue, tim, sue, tim, tim, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that the people with infinite turns really do have infinite turns.
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
    }

    [TestMethod]
    // Scenario: Try to get the next person from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message.
    // Defect(s) Found: No defect - the GetNextPerson() method correctly throws InvalidOperationException("No one in the queue.") when the queue is empty.
    // How it was verified: The existing code already had the proper empty queue check using "if (_people.IsEmpty())" and threw the correct exception with the correct message, so no fix was needed for this test case.
    public void TestTakingTurnsQueue_Empty()
    {
        var players = new TakingTurnsQueue();

        try
        {
            players.GetNextPerson();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("No one in the queue.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }
}