using System.Text;

namespace AaDS.DataStructures.Tree;

class ExpressionTree
{
    class Node
    {
        public char Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(char value) => Value = value;
    }

    readonly string _expression;
    Node? _root;

    Stack.Stack<Node> valueStack, operatorStack;

    public ExpressionTree(string expression)
    {
        _expression = expression;
        valueStack = new();
        operatorStack = new();
    }

    int Priority(char op) => op switch
    {
        '^' => 3,
        '*' or '/' or ':' => 2,
        '+' or '-' => 1,
        _ => 0,
    };

    bool IsOperand(char token) => char.IsLetterOrDigit(token);

    bool IsOperator(char token) => token is '+' or '-' or '*' or '/' or '^' or ':';

    Node PopParent()
    {
        Node topOperator = operatorStack.Pop();
        topOperator.Right = valueStack.Pop();
        topOperator.Left = valueStack.Pop();
        return topOperator;
    }
    
    public void BuildTree()
    {
        foreach (char token in _expression)
        {
            if (IsOperand(token))
                valueStack.Push(new(token));
            else if (IsOperator(token))
            {
                Node currentNode = new(token);
                while (operatorStack.Count > 0 && Priority(operatorStack.Peek().Value) >= Priority(token))
                    valueStack.Push(PopParent());
                operatorStack.Push(currentNode);
            }
            else switch (token)
            {
                case '(':
                    operatorStack.Push(new(token));
                    break;
                case ')':
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek().Value != '(')
                        valueStack.Push(PopParent());
                    operatorStack.Pop(); // Pop the "("
                    break;
                }
            }
        }

        while (operatorStack.Count > 0)
            valueStack.Push(PopParent());

        if (valueStack.Count != 1)
            throw new InvalidOperationException("Invalid expression");

        _root = valueStack.Pop();
    }

    public string PrefixTraversal()
    {
        StringBuilder sb = new();
        PrefixTraversal(_root, sb);
        return sb.ToString();
    }
    
    void PrefixTraversal(Node? node, StringBuilder sb)
    {
        if (node != null)
        {
            sb.Append(node.Value);
            PrefixTraversal(node.Left, sb);
            PrefixTraversal(node.Right, sb);
        }
    }

    public string InfixTraversal() => _expression;

    public string PostfixTraversal()
    {
        StringBuilder sb = new();
        PostfixTraversal(_root, sb);
        return sb.ToString();
    }
    
    void PostfixTraversal(Node? node, StringBuilder sb)
    {
        if (node != null)
        {
            PostfixTraversal(node.Left, sb);
            PostfixTraversal(node.Right, sb);
            sb.Append(node.Value);
        }
    }
}