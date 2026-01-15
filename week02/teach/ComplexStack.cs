public static class ComplexStack {
    public static bool DoSomethingComplicated(string line) {
        var stack = new Stack<char>();
        foreach (var item in line) {
            if (item is '(' or '[' or '{') {
                stack.Push(item);
            }
            else if (item is ')') {
                if (stack.Count == 0 || stack.Pop() != '(')
                    return false;
            }
            else if (item is ']') {
                if (stack.Count == 0 || stack.Pop() != '[')
                    return false;
            }
            else if (item is '}') {
                if (stack.Count == 0 || stack.Pop() != '{')
                    return false;
            }
        }

        return stack.Count == 0;
    }
    // Here's my comment listing for the inputs we were asked to do on the team project assignment for this week.
    
    // Sample Input 1: (a == 3 or (b == 5 and c == 6))
    // Solution: True - All parentheses are properly matched and balanced and the stack was empty at the end.

    // Sample Input 2: (students[i].Grade > 80 and students[i].Grade < 90)
    // Solution: False - wrong opening bracket used; mismatched brackets/parentheses will make this fail.

    // Sample Input 3: (robot[id + 1].Execute(.Pass() || (!robot[id * (2 + i)].Alive && stormy) || (robot[id - 1].Alive && lavaFlowing))
    // Solution: False - The outermost opening parenthesis is never closed; mismatched brackets/parentheses wwill not allow the stack to empty.
}