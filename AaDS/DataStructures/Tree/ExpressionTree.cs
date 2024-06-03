using System.Text;

namespace AaDS.DataStructures.Tree;

//For writing expressions without spaces and get unary minus/plus, check RPN class
class ExpressionTree
{
    class Node
    {
        public string Value { get; set; } 
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(string value) => Value = value;
    }

    public string Expression { get; set; }
    Node? _root;

    Stack<Node> _valueStack, _operatorStack;

    public ExpressionTree(string expression)
    {
        Expression = expression;
        _valueStack = [];
        _operatorStack = [];
    }

    int Priority(string op) => op switch // Changed type to string
    {
        "^" => 3,
        "*" or "/" or ":" => 2,
        "+" or "-" => 1,
        _ => 0,
    };

    bool IsOperand(string token) => double.TryParse(token, out _);

    bool IsOperator(string token) => token is "+" or "-" or "*" or "/" or "^" or ":";

    Node PopParent()
    {
        var topOperator = _operatorStack.Pop();
        topOperator.Right = _valueStack.Pop();
        topOperator.Left = _valueStack.Pop();
        return topOperator;
    }

    public void BuildTree()
    {
        //should be like ( 1 + 2 ) ^ 3
        string[] tokens = Expression.Split(' ', StringSplitOptions.RemoveEmptyEntries); 
        foreach (string token in tokens)
        {
            if (IsOperand(token))
            {
                _valueStack.Push(new Node(token));
            }
            else if (IsOperator(token))
            {
                var currentNode = new Node(token);
                while (_operatorStack.Count > 0 && Priority(_operatorStack.Peek().Value) >= Priority(token))
                {
                    _valueStack.Push(PopParent());
                }
                _operatorStack.Push(currentNode);
            }
            else switch (token)
            {
                case "(":
                    _operatorStack.Push(new Node(token));
                    break;
                case ")":
                {
                    while (_operatorStack.Count > 0 && _operatorStack.Peek().Value != "(")
                    {
                        _valueStack.Push(PopParent());
                    }
                    _operatorStack.Pop(); // Pop the "("
                    break;
                }
            }
        }

        while (_operatorStack.Count > 0)
            _valueStack.Push(PopParent());

        if (_valueStack.Count != 1)
            throw new InvalidOperationException("Invalid expression");

        _root = _valueStack.Pop();
    }

    public double Evaluate()
    {
        return Evaluate(_root);
            
        double Evaluate(Node? node)
        {
            if (node == null)
                throw new InvalidOperationException("Invalid expression");

            if (IsOperand(node.Value))
                return double.Parse(node.Value); // Convert string to double
            else
            {
                double leftValue = Evaluate(node.Left);
                double rightValue = Evaluate(node.Right);
                return Calculate(leftValue, rightValue, node.Value); // Perform calculation
            }
        }
    }

    double Calculate(double left, double right, string op) =>
        op switch
        {
            "+" => left + right,
            "-" => left - right,
            "*" => left * right,
            "/" or ":" => left / right,
            "^" => Math.Pow(left, right),
            _ => throw new ArgumentException("Invalid operator"),
        };

    public string PrefixTraversal()
    {
        StringBuilder sb = new StringBuilder();
        Traversal(_root);
        return sb.ToString();

        void Traversal(Node? node)
        {
            if (node != null)
            {
                sb.Append(node.Value).Append(' ');
                Traversal(node.Left);
                Traversal(node.Right);
            }
        }
    }

    public string InfixTraversal() => Expression;

    public string PostfixTraversal()
    {
        StringBuilder sb = new StringBuilder();
        Traversal(_root);
        return sb.ToString();

        void Traversal(Node? node)
        {
            if (node != null)
            {
                Traversal(node.Left);
                Traversal(node.Right);
                sb.Append(node.Value).Append(' ');
            }
        }
    }
}