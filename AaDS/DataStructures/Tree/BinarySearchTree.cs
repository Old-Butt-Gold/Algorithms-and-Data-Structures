using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Tree;

class BinarySearchTree<TValue> where TValue : IComparable<TValue>
{
    public int Count { get; set; }
    public bool IsEmpty => Count == 0;
    BSTNode<TValue>? Root;
    public BinarySearchTree() { }

    public BinarySearchTree(TValue value) => (Root, Count) = (new(value), 1);

    public BinarySearchTree(IEnumerable<TValue>? collection)
    {
        if (collection != null)
        {
            var sortedList = collection.Distinct().ToList();
            sortedList.Sort();
            Count += sortedList.Count;
            Root = CreateTreeFromList(sortedList, 0, sortedList.Count - 1);
        }
    }

    BSTNode<TValue>? CreateTreeFromList(List<TValue> list, int start, int end)
    {
        if (end < start) return null;
        int mid = (start + end) / 2;
        return new(list[mid])
        {
            Left = CreateTreeFromList(list, start, mid - 1),
            Right = CreateTreeFromList(list, mid + 1, end)
        };
    }

    public bool Add(TValue value)
    {
        if (!Contains(value))
        {
            Root = AddToNode(Root, value);
            Count++;
            return true;
        }

        return false;
    }
    
    BSTNode<TValue>? AddToNode(BSTNode<TValue>? node, TValue value)
    {
        if (node == null)
            return new(value);

        int comparison = value.CompareTo(node.Value);
        if (comparison < 0)
            node.Left = AddToNode(node.Left, value);
        else if (comparison > 0)
            node.Right = AddToNode(node.Right, value);
        return node;
    }

    public BSTNode<TValue>? Search(TValue value) => SearchNode(Root, value);

    public bool Contains(TValue value) => Search(value) != null;
    
    BSTNode<TValue>? SearchNode(BSTNode<TValue>? Root, TValue value)
    {
        if (Root == null) return null;
        
        int comparison = value.CompareTo(Root.Value);
        if (comparison == 0)
            return Root;
        
        return SearchNode(comparison < 0 ? Root.Left : Root.Right, value);
    }

    public bool Remove(TValue value)
    {
        if (Contains(value))
        {
            Root = RemoveNode(Root, value);
            Count--;
            return true;
        }

        return false;
    }

    BSTNode<TValue>? RemoveNode(BSTNode<TValue>? root, TValue value)
    {
        if (root is null)
            return root;
        int comparison = value.CompareTo(root.Value);

        if (comparison < 0)
            root.Left = RemoveNode(root.Left, value);
        else if (comparison > 0)
            root.Right = RemoveNode(root.Right, value);
        else
        {
            if (root.Left == null)
                return root.Right;

            if (root.Right == null)
                return root.Left;

            root.Value = FindMinNode(root.Right)!.Value;
            root.Right = RemoveNode(root.Right, root.Value);
        }

        return root;
    }
    
    BSTNode<TValue>? FindMinNode(BSTNode<TValue>? node) => node?.Left == null ? node : FindMinNode(node.Left);
    
    public TValue FindMinValue() => FindMinValueRecursive(Root);

    TValue FindMinValueRecursive(BSTNode<TValue>? node) => node.Left == null ? node.Value : FindMinValueRecursive(node.Left);

    public TValue FindMaxValue() => FindMaxValueRecursive(Root);

    TValue FindMaxValueRecursive(BSTNode<TValue>? node) => node.Right == null ? node.Value : FindMaxValueRecursive(node.Right);

    public int CountIncompleteNodes() => CountIncompleteNodes(Root);

    int CountIncompleteNodes(BSTNode<TValue> bstNode)
    {
        if (Root is null) 
            return 0;

        int count = 0;

        if ((Root.Left is null && Root.Right != null) || (Root.Right is null && Root.Left != null))
        {
            count++;
        }

        count += CountIncompleteNodes();
        count += CountIncompleteNodes();

        return count;
    }
    
    public bool RemoveMin()
    {
        if (Root == null)
            return false;

        Root = RemoveMinNode(Root);
        Count--;
        return true;
    }

    BSTNode<TValue>? RemoveMinNode(BSTNode<TValue>? node)
    {
        if (node?.Left == null)
            return node?.Right;

        node.Left = RemoveMinNode(node.Left);
        return node;
    }

    public bool RemoveMax()
    {
        if (Root == null)
            return false;

        Root = RemoveMaxNode(Root);
        Count--;
        return true;
    }

    BSTNode<TValue>? RemoveMaxNode(BSTNode<TValue>? node)
    {
        if (node?.Right == null)
            return node?.Left;

        node.Right = RemoveMaxNode(node.Right);
        return node;
    }
    
    public int GetHeight() => GetHeight(Root);

    int GetHeight(BSTNode<TValue>? node) => 
        node == null ? 0 : Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

    public bool IsBalanced() => IsBalanced(Root);

    bool IsBalanced(BSTNode<TValue>? node)
    {
        if (node == null)
            return true;

        int leftHeight = GetHeight(node.Left);
        int rightHeight = GetHeight(node.Right);

        return Math.Abs(leftHeight - rightHeight) <= 1 && IsBalanced(node.Left) && IsBalanced(node.Right);
    }
    
    public int CountLeaves() => CountLeaves(Root);

    int CountLeaves(BSTNode<TValue>? node)
    {
        if (node == null) return 0;
    
        if (node.Left == null && node.Right == null)
            return 1;
    
        return CountLeaves(node.Left) + CountLeaves(node.Right);
    }
    
    public int CountNodes() => CountNodes(Root);
    int CountNodes(BSTNode<TValue>? node) => node == null ? 0 : 1 + CountNodes(node.Left) + CountNodes(node.Right);
    
    void Invert() => Invert(Root); //как тестовое

    private void Invert(BSTNode<TValue>? node)
    {
        if (node == null)
            return;

        (node.Left, node.Right) = (node.Right, node.Left);

        Invert(node.Left);
        Invert(node.Right);
    }

    public int CountNodesAtLevel(int level) => CountNodesAtLevel(Root, level, 0);

    int CountNodesAtLevel(BSTNode<TValue>? node, int targetLevel, int currentLevel)
    {
        if (node == null)
            return 0;

        if (currentLevel == targetLevel)
            return 1;

        return CountNodesAtLevel(node.Left, targetLevel, currentLevel + 1) +
               CountNodesAtLevel(node.Right, targetLevel, currentLevel + 1);
    }
    
    public BSTNode<TValue>? FindPredecessor(TValue value) => FindPredecessor(Root, value);
    BSTNode<TValue>? FindPredecessor(BSTNode<TValue>? node, TValue value)
    {
        if (node == null) return null;

        if (node.Value.CompareTo(value) >= 0)
            return FindPredecessor(node.Left, value);

        return FindPredecessor(node.Right, value) ?? node;
    }

    public BSTNode<TValue>? FindSuccessor(TValue value) => FindSuccessor(Root, value);
    BSTNode<TValue>? FindSuccessor(BSTNode<TValue>? node, TValue value)
    {
        if (node == null) return null;

        if (node.Value.CompareTo(value) <= 0)
            return FindSuccessor(node.Right, value);

        return FindSuccessor(node.Left, value) ?? node;
    }

    public List<TValue> PreOrderTraversal()
    {
        List<TValue> list = new();
        PreOrderTraversal(Root, list.Add);
        return list;
    }

    void PreOrderTraversal(BSTNode<TValue>? node, Action<TValue> action)
    {
        if (node == null) return;
    
        action(node.Value);
        PreOrderTraversal(node.Left, action);
        PreOrderTraversal(node.Right, action);
    }

    public List<TValue> InOrderTraversal()
    {
        List<TValue> list = new();
        InOrderTraversal(Root, list.Add);
        return list;
    }

    void InOrderTraversal(BSTNode<TValue>? node, Action<TValue> action)
    {
        if (node == null) return;
        InOrderTraversal(node.Left, action);
        action(node.Value);
        InOrderTraversal(node.Right, action);
    }

    public List<TValue> ReverseInOrderTraversal()
    {
        List<TValue> list = new();
        ReverseInOrderTraversal(Root, list.Add);
        return list;
    }

    void ReverseInOrderTraversal(BSTNode<TValue>? node, Action<TValue> action)
    {
        if (node == null) return;
        
        ReverseInOrderTraversal(node.Right, action);
        action(node.Value);
        ReverseInOrderTraversal(node.Left, action);
    }

    public List<TValue> PostOrderTraversal()
    {
        List<TValue> list = new();
        PostOrderTraversal(Root, list.Add);
        return list;
    }

    void PostOrderTraversal(BSTNode<TValue>? node, Action<TValue> action)
    {
        if (node == null) return;
    
        PostOrderTraversal(node.Left, action);
        PostOrderTraversal(node.Right, action);
        action(node.Value);
    }

    public List<TValue> LevelOrderTraversal()
    {
        List<TValue> list = new();
        LevelOrderTraversal(list.Add);
        return list;
    }
    void LevelOrderTraversal(Action<TValue> action) //Breadth-first traversal (в ширину)
    {
        if (Root == null) return;

        Queue.Queue<BSTNode<TValue>> queue = new();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            BSTNode<TValue> node = queue.Dequeue();
            action(node.Value);

            if (node.Left != null)
                queue.Enqueue(node.Left);

            if (node.Right != null)
                queue.Enqueue(node.Right);
        }
    }

    public void Clear()
    {
        ClearRecursive(Root);
        Root = null;
        Count = 0;
    }

    void ClearRecursive(BSTNode<TValue>? node)
    {
        if (node == null)
            return;

        ClearRecursive(node.Left);
        ClearRecursive(node.Right);

        node.Left = null;
        node.Right = null;
    }
    
    public void BalanceTree()
    {
        List<TValue> list = InOrderTraversal();
        Clear();
        Root = CreateTreeFromList(list, 0, list.Count - 1);
        Count += list.Count;
    }
    
    public void PrintTree() => PrintTree(Root, "", true);

    void PrintTree(BSTNode<TValue>? node, string prefix, bool isTail)
    {
        if (node == null) return;

        Console.Write(prefix + (isTail ? "└── " : "├── ") + node.Value + "\n");
        PrintTree(node.Left, prefix + (isTail ? "    " : "│   "), false);
        PrintTree(node.Right, prefix + (isTail ? "    " : "│   "), true);
    }

    public BinarySearchTree<TValue> Clone()
    {
        BinarySearchTree<TValue> clonedTree = new();

        if (Root != null)
        {
            clonedTree.Root = CloneNode(Root);
            clonedTree.Count = Count;
        }

        return clonedTree;
    }

    BSTNode<TValue>? CloneNode(BSTNode<TValue>? node)
    {
        if (node == null)
            return null;

        return new(node.Value)
        {
            Left = CloneNode(node.Left),
            Right = CloneNode(node.Right)
        };
    }
}