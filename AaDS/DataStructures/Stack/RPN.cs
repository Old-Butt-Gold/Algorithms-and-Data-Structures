using System.Text;

namespace AaDS.DataStructures.Stack;

/// <summary>
/// Reverse Polish Notation with basic calculator methods
/// </summary>
class RPN
{
    // Method returns true if the character is a delimiter (space or equals)
    static bool IsDelimiter(char c) => " =".Contains(c);

    // Method returns true if the character is an operator
    static bool IsOperator(char c) => "+-/^*()".Contains(c);

    // Method returns the priority of an operator
    static int GetPriority(char s) =>
        s switch
        {
            '(' => 0,
            ')' => 1,
            '+' or '-' => 2,
            '*' or '/' => 4,
            '^' => 5,
            _ => 6
        };

    static string FilterString(string input)
    {
        int counter = 0;
        int i = input.Length - 1;
        while (i > -1 && input[i] != ')' && (IsOperator(input[i]) || IsDelimiter(input[i])))
        {
            counter++;
            i--;
        }

        return counter == 0 ? input : input[..(input.Length - counter + 1)];
    }

    public static string ConvertToPostfix(string input)
    {
        input = FilterString(input);
        StringBuilder output = new(); // String to store the expression
        var operStack = new Stack<char>(); // Stack to store operators
        bool lastCharWasOperator = true; // Track if the last character was an operator
            
        for (int i = 0; i < input.Length; i++) // For each character in the input string
        {
            // Skip delimiters
            if (IsDelimiter(input[i]))
            {
                continue; // Move to the next character
            }

            // If the character is a digit or a negative sign followed by a digit, read the entire number
            if (char.IsDigit(input[i]) || (input[i] == '-' && lastCharWasOperator && i + 1 < input.Length && char.IsDigit(input[i + 1])))
            {
                // Read until delimiter or operator to get the number
                if (input[i] == '-')
                {
                    output.Append(input[i]); // Add the negative sign
                    i++;
                }

                while (i < input.Length && (!IsDelimiter(input[i]) && !IsOperator(input[i]) || input[i] == ','))
                {
                    output.Append(input[i]); // Add each digit to our string
                    i++; // Move to the next character
                }

                output.Append(' '); // Append a space after the number
                i--; // Move back one character
                lastCharWasOperator = false;
            }
            // If the character is an operator
            else if (IsOperator(input[i])) // If operator
            {
                if (input[i] == '(') // If opening bracket
                {
                    operStack.Push(input[i]); // Push it onto the stack
                    lastCharWasOperator = true;
                }
                else if (input[i] == ')') // If closing bracket
                {
                    // Pop all operators until the opening bracket
                    while (operStack.Count > 0 && operStack.Peek() != '(')
                    {
                        output.Append(operStack.Pop() + " ");
                    }

                    if (operStack.Count == 0)
                    {
                        throw new ArgumentException("Mismatched parentheses");
                    }

                    operStack.Pop(); // Remove the opening bracket from the stack

                    lastCharWasOperator = false;
                }
                else // Any other operator
                {
                    // Check for cases like "-(3 - (- (4 + 5) ))"
                    if (input[i] == '-' && lastCharWasOperator)
                    {
                        // Push '0' to handle unary minus
                        output.Append("0 ");
                    }
                        
                    while (operStack.Count > 0 && GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                    {
                        output.Append(operStack.Pop() + " "); // Pop operators with higher or equal priority
                    }

                    operStack.Push(input[i]); // Push the current operator onto the stack
                    lastCharWasOperator = true;
                }
            }
            else
            {
                throw new ArgumentException($"Invalid character in input: {input[i]}");
            }
        }

        // Pop all remaining operators from the stack
        while (operStack.Count > 0)
        {
            char op = operStack.Pop();
            if (op is '(' or ')')
            {
                throw new ArgumentException("Mismatched parentheses");
            }

            output.Append(op + " ");
        }

        return output.ToString().Trim(); // Return the postfix expression
    }

    public static double Calculate(string input)
    {
        double result = 0; // Result
        var stack = new Stack<double>(); // Stack for solving
        for (int i = 0; i < input.Length; i++) // For each character in the string
        {
            // If the character is a digit or a negative sign followed by a digit
            if (char.IsDigit(input[i]) || (input[i] == '-' && i + 1 < input.Length && char.IsDigit(input[i + 1])))
            {
                StringBuilder numFormer = new();

                // Read the number (including negative sign if present)
                if (input[i] == '-')
                {
                    numFormer.Append(input[i]);
                    i++;
                }

                while (i < input.Length && (!IsDelimiter(input[i]) && !IsOperator(input[i]) || input[i] == '.')) // Until delimiter
                {
                    numFormer.Append(input[i]); // Add
                    i++;
                }

                stack.Push(double.Parse(numFormer.ToString())); // Push onto the stack
                i--;
            }
            else if (IsOperator(input[i])) // If the character is an operator
            {
                // Pop the last two values from the stack
                var first = stack.Pop();
                var second = stack.Pop();

                switch (input[i]) // Perform the operation according to the operator
                {
                    case '+':
                        result = second + first;
                        break;
                    case '-':
                        result = second - first;
                        break;
                    case '*':
                        result = second * first;
                        break;
                    case '/':
                        const double eps = 1e-9;
                        if (Math.Abs(first) < eps)
                        {
                            throw new DivideByZeroException("Division by zero");
                        }
                        result = second / first;
                        break;
                    case '^':
                        result = Math.Pow(second, first);
                        break;
                }

                stack.Push(result); // Push the result back onto the stack
            }
            else if (!IsDelimiter(input[i]))
            {
                throw new ArgumentException($"Invalid character in input: {input[i]}");
            }
        }

        if (stack.Count != 1)
        {
            throw new InvalidOperationException("The user input has too many values");
        }

        return stack.Pop(); // Get the result of all calculations from the stack and return it
    }
}