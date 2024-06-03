using System.Collections;
using System.Text;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Stack;

class Stack<T> : IEnumerable<T>
{
    Node<T>? _head;
    public int Count { get; private set; }
 
    public bool IsEmpty => Count == 0;
    
    public void Push(T item)
    {
        _head = new(item) { Next = _head };
        Count++;
    }
    
    public T Pop()
    {
        if (IsEmpty) throw new InvalidOperationException("Стек пуст");
        Node<T>? temp = _head;
        _head = _head?.Next;
        Count--;
        return temp!.Data;
    }
    
    public bool TryPop(out T? result)
    {
        if (!IsEmpty)
        {
            result = Pop();
            return true;
        }
        result = default;
        return false;
    }
    
    public T Peek()
    {
        if (IsEmpty) throw new InvalidOperationException("Стек пуст");
        return _head!.Data;
    }

    public bool TryPeek(out T? result)
    {
        result = _head is null ? default : _head.Data;
        return !IsEmpty;
    }

    public void Clear() => (_head, Count) = (null, 0);

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? current = _head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    #region OtherAlgos

    /// <summary>
    /// Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.
    /// An input string is valid if:
    /// Open brackets must be closed by the same type of brackets.
    /// Open brackets must be closed in the correct order.
    /// Every close bracket has a corresponding open bracket of the same type.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    bool IsValid(string s)
    {
        Stack<int> stack = [];
        
        foreach (var chr in s)
        {
            if (chr is '[' or '(' or '{')
            {
                stack.Push(chr);
            }
            else if (chr is ']' or ')' or '}')
            {
                if (stack.IsEmpty)
                {
                    return false;
                }

                var popChar = stack.Pop();
                if ((chr == ')' && popChar != '(')
                    || (chr == ']' && popChar != '[')
                    || (chr == '}' && popChar != '{'))
                {
                    return false;
                }
            }
        }

        return stack.IsEmpty;
    }

    /// <summary>
    /// You are given an array of strings tokens that represents an arithmetic expression in a Reverse Polish Notation.
    /// Evaluate the expression. Return an integer that represents the value of the expression.
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns></returns>
    public double EvalRPN(string[] tokens)
    {
        Stack<double> stack = [];

        foreach (var token in tokens)
        {
            double result = 0;
            if (int.TryParse(token, out var num))
            {
                stack.Push(num);
            }
            else if (IsOperator(token))
            {
                //you should filter input string (just like in RPN class), so that's the last characters doesn't have any Operators/Delimiters (except ')') at the end
                //Also there should be no more than one unary operation before the first symbol: like --1, and not ----1
                //also to handle unary minus use 0 just like in RPN

                var first = stack.Pop();
                var second = stack.Pop();

                switch (token) //И производим над ними действие, согласно оператору
                {
                    case "+":
                        result = first + second;
                        break;
                    case "-":
                        result = first - second;
                        break;
                    case "*":
                        result = first * second;
                        break;
                    case "/":
                        result = first / second;
                        break;
                    case "^":
                        result = (int)Math.Pow(first, second);
                        break;
                }

                stack.Push(result);
            }


        }

        return stack.Pop();
        
        bool IsOperator(string с) => "+-/^*()".Contains(с);
    }

    /// <summary>
    /// Given an absolute path for a Unix-style file system, which begins with a slash '/', transform this path into its simplified canonical path.
    /// https://leetcode.com/problems/simplify-path/description/?envType=study-plan-v2&envId=top-interview-150
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    string SimplifyPath(string path)
    {
        System.Collections.Generic.Stack<string> stack = [];
        var parts = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            if (part == "..")
            {
                if (stack.Count > 0)
                {
                    stack.Pop();
                }
            }
            else if (part != ".")
            {
                stack.Push(part);
            }
        }

        var result = new StringBuilder();
        foreach (var dir in stack.Reverse())
        {
            result.Append('/').Append(dir);
        }

        return result.Length == 0 ? "/" : result.ToString();
    }

    #endregion
}