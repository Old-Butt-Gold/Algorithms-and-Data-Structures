using System.Text;

namespace AaDS.DataStructures.Stack;

class RPN
{
    Stack<char> _stack;

    public RPN()
    {
        _stack = new();
        _stack.Push('\0');
    }

    public int Rang { get; private set; }

    int GetStackPriority(char character)
    {
        switch (character)
        {
            case '+' or '-':
                return 2;
            case '/' or '*':
                return 4;
            case '(' or '\0':
                return 0;
            case >= 'a' and <= 'z':
                return 8;
            case '^':
                return 5;
            default:
                throw new ArgumentException("Invalid character");
        }
    }

    int GetPriority(char character)
    {
        switch (character)
        {
            case '+' or '-':
                return 1;
            case '/' or '*':
                return 3;
            case '(':
                return 9;
            case >= 'a' and <= 'z':
                return 7;
            case '^':
                return 6;
            default:
                throw new ArgumentException("Invalid character");
        }
    }

    void CalculationOfRang()
    {
        Rang = _stack.Peek() is >= 'a' and <= 'z'? Rang + 1 : Rang - 1;
    }
    
    public string ConvertToPostfix(string expression)
    {
        StringBuilder result = new();
        Rang = 0;
        for (int i = 0; i < expression.Length; i++)
        {
            if (expression[i] == ')')
            {
                while (_stack.Peek() != '(')
                {
                    CalculationOfRang();
                    result.Append(_stack.Pop());
                }
                _stack.Pop();
            }
            else
            {
                if (GetPriority(expression[i]) > GetStackPriority(_stack.Peek()))
                    _stack.Push(expression[i]);
                else
                {
                    while (!_stack.IsEmpty && GetPriority(expression[i]) <= GetStackPriority(_stack.Peek()))
                    {
                        CalculationOfRang();
                        result.Append(_stack.Pop());
                    }
                    _stack.Push(expression[i]);
                }
            }
        }

        while (_stack.Peek() != '\0')
        {
            CalculationOfRang();
            result.Append(_stack.Pop());
        }

        return result.ToString();
    }
    
}