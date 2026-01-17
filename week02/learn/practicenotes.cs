using System;


public static class MysteryStack1
{
    /// <summary>
/// PURPOSE: This function reverses a string using a stack data structure.
/// 
/// HOW IT WORKS:
/// 1. Creates an empty stack to store characters
/// 2. Iterates through each character in the input text and pushes it onto the stack
/// 3. Pops all characters from the stack one by one to build the result string
/// 4. Returns the reversed string
/// 
/// WHY A STACK IS USEFUL:
/// A stack operates on the LIFO (Last-In-First-Out) principle. The last character pushed
/// onto the stack is the first one popped off, naturally reversing the order of characters.
/// This is an elegant solution because:
/// - It automatically handles the reversal through its LIFO behavior
/// - No manual indexing or complex logic needed
/// - Demonstrates a practical application of stack data structures
/// </summary>
    public static string Run(string text)
    {
        // Create a stack to temporarily store characters
        var stack = new Stack<char>();
        
        // Push each character from the input text onto the stack in order
        foreach (var letter in text)
            stack.Push(letter);

        // Pop characters from the stack (which retrieves them in reverse order)
        // and build the result string
        var result = "";
        while (stack.Count > 0)
            result += stack.Pop();

        // Return the reversed string
        return result;
    }
}

/// <summary>
/// PURPOSE: This function implements a Reverse Polish Notation (RPN) calculator.
/// It evaluates mathematical expressions where operators come AFTER operands.
/// 
/// HOW IT WORKS:
/// 1. Splits the input text into tokens (numbers and operators separated by spaces)
/// 2. For each token:
///    - If it's a number: push it onto the stack
///    - If it's an operator (+, -, *, /): pop two operands, perform operation, push result
///    - Otherwise: throw an error
/// 3. At the end, exactly one value should remain on the stack (the final result)
/// 4. Returns the final result
/// 
/// WHY A STACK IS USEFUL:
/// A stack naturally handles the order of operations in RPN:
/// - Operands are pushed as they arrive
/// - When an operator is encountered, the two most recent operands (top of stack) are popped
/// - The LIFO property ensures correct precedence without needing parentheses or rules
/// - This is how calculators and compilers evaluate expressions efficiently
/// </summary>
public static class MysteryStack2
  {
      private static bool IsFloat(string text)
      {
          return float.TryParse(text, out _);
      }
  
      public static float Run(string text)
      {
          // Create a stack to store operands and intermediate results
          var stack = new Stack<float>();
          
          // Split the input by spaces to process each token
          foreach (var item in text.Split(' '))
          {
              // Check if the token is an operator
              if (item == "+" || item == "-" || item == "*" || item == "/")
              {
                  // Error Case 1: Not enough operands for the operation
                  if (stack.Count < 2)
                      throw new ApplicationException("Invalid Case 1!");

                  // Pop the two operands (order matters: op2 is popped first)
                  var op2 = stack.Pop();
                  var op1 = stack.Pop();
                  float res;
                  
                  // Perform the appropriate operation
                  if (item == "+")
                  {
                      res = op1 + op2;
                  }
                  else if (item == "-")
                  {
                      res = op1 - op2;
                  }
                  else if (item == "*")
                  {
                      res = op1 * op2;
                  }
                  else
                  {
                      // Error Case 2: Division by zero
                      if (op2 == 0)
                          throw new ApplicationException("Invalid Case 2!");

                      res = op1 / op2;
                  }

                  // Push the result back onto the stack for use in later operations
                  stack.Push(res);
              }
              // Check if the token is a valid number
              else if (IsFloat(item))
              {
                  // Push the number onto the stack
                  stack.Push(float.Parse(item));
              }
              // Handle empty tokens (caused by multiple spaces)
              else if (item == "")
              {
                  // Skip empty tokens silently
              }
              // Error Case 3: Invalid token (not a number, not an operator, not empty)
              else
              {
                  throw new ApplicationException("Invalid Case 3!");
              }
          }

          // Error Case 4: Final stack should have exactly 1 value (the result)
          // Multiple values mean too few operators; zero values mean malformed expression
          if (stack.Count != 1)
              throw new ApplicationException("Invalid Case 4!");

          // Return the final calculated result
          return stack.Pop();
      }
  }