//Аналог TreeSet на AVLTree

using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Tree;

public class AVLTree<TValue>
{
    AVLNode<TValue>? Root;

    public int Count { get; set; }

    public bool IsEmpty => Count == 0;

    public AVLTree() { }
    
    public AVLTree(IEnumerable<TValue>? collection)
    {
        if (collection != null)
        {
            var sortedList = collection.Distinct().ToList();
            sortedList.Sort();
            Root = CreateTreeFromList(sortedList, 0, sortedList.Count - 1);
            Count += sortedList.Count;
        }
    }
    
    AVLNode<TValue>? CreateTreeFromList(List<TValue> list, int start, int end)
    {
        if (end < start) return null;
        int mid = (start + end) / 2;
        return new(list[mid])
        {
            Left = CreateTreeFromList(list, start, mid - 1),
            Right = CreateTreeFromList(list, mid + 1, end)
        };
    }
    
    AVLNode<TValue>? RotateRight(AVLNode<TValue>? node)
    {
        AVLNode<TValue>? newRoot = node!.Left;
        node!.Left = newRoot!.Right;
        newRoot.Right = node;
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        newRoot.Height = 1 + Math.Max(Height(newRoot.Left), Height(newRoot.Right));
        return newRoot;
    }

    AVLNode<TValue>? RotateLeft(AVLNode<TValue>? node)
    {
        AVLNode<TValue>? newRoot = node!.Right;
        node.Right = newRoot!.Left;
        newRoot.Left = node;
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        newRoot.Height = 1 + Math.Max(Height(newRoot.Left), Height(newRoot.Right));
        return newRoot;
    }

    int Height(AVLNode<TValue>? node) => node?.Height ?? 0;

    int BalanceFactor(AVLNode<TValue>? node) => node == null ? 0 : Height(node.Left) - Height(node.Right);

    AVLNode<TValue>? Balance(AVLNode<TValue>? node)
    {
        if (node == null)
            return node;

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        int balanceFactor = BalanceFactor(node);
    
        if (balanceFactor > 1)
        {
            if (BalanceFactor(node.Left) < 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        if (balanceFactor < -1)
        {
            if (BalanceFactor(node.Right) > 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }
    
    public bool Add(TValue value)
    {
        if (!Contains(value))
        {
            Root = Insert(Root, value);
            Count++;
            return true;
        }

        return false;
    }

    AVLNode<TValue>? Insert(AVLNode<TValue>? node, TValue value)
    {
        if (node == null)
            return new(value);
        int comparison = Comparer<TValue>.Default.Compare(value, node.Value);
        if (comparison < 0)
            node.Left = Insert(node.Left, value);
        else if (comparison > 0)
            node.Right = Insert(node.Right, value);

        return Balance(node);
    }

    public bool Remove(TValue value)
    {
        if (Contains(value))
        {
            Root = Delete(Root, value);
            Count--;
            return true;
        }

        return false;
    }

    AVLNode<TValue>? Delete(AVLNode<TValue>? node, TValue key)
    {
        if (node == null)
            return node;
        int comparison = Comparer<TValue>.Default.Compare(key, node.Value);
        if (comparison < 0)
            node.Left = Delete(node.Left, key);
        else if (comparison > 0)
            node.Right = Delete(node.Right, key);
        else
        {
            if (node.Left == null)
                return node.Right;
            if (node.Right == null)
                return node.Left;
            
            AVLNode<TValue> minNode = FindMinNode(node.Right)!;
            node.Value = minNode!.Value;
            node.Right = RemoveMinNode(node.Right);
        }

        return Balance(node);
    }

    AVLNode<TValue>? FindMinNode(AVLNode<TValue>? node) => node?.Left == null ? node : FindMinNode(node.Left);

    public TValue FindMinValue() => FindMinValueRecursive(Root);

    TValue FindMinValueRecursive(AVLNode<TValue>? node) => node.Left == null ? node.Value : FindMinValueRecursive(node.Left);

    public TValue FindMaxValue() => FindMaxValueRecursive(Root);

    TValue FindMaxValueRecursive(AVLNode<TValue>? node) => node.Right == null ? node.Value : FindMaxValueRecursive(node.Right);
    
    public bool RemoveMin()
    {
        if (Root is null)
            return false;
        Root = RemoveMinNode(Root);
        return true;
    }

    AVLNode<TValue>? RemoveMinNode(AVLNode<TValue>? node)
    {
        if (node?.Left == null)
            return node?.Right;

        node.Left = RemoveMinNode(node.Left);
        return Balance(node);
    }

    public bool RemoveMax()
    {
        if (Root == null)
            return false;

        Root = RemoveMaxNode(Root);
        return true;
    }

    AVLNode<TValue>? RemoveMaxNode(AVLNode<TValue>? node)
    {
        if (node?.Right == null)
            return node?.Left;

        node.Right = RemoveMaxNode(node.Right);
        return Balance(node);
    }

    public bool Contains(TValue value) => Search(Root, value);

    bool Search(AVLNode<TValue>? node, TValue value)
    {
        if (node == null)
            return false;
        int comparison = Comparer<TValue>.Default.Compare(value, node.Value);
        if (comparison == 0)
            return true;
        return Search(comparison < 0 ? node.Left : node.Right, value);
    }

    public int TreeHeight() => Height(Root);
    
    public int CountLeaves() => CountLeaves(Root);

    int CountLeaves(AVLNode<TValue>? node)
    {
        if (node == null) return 0;
    
        if (node.Left == null && node.Right == null)
            return 1;
    
        return CountLeaves(node.Left) + CountLeaves(node.Right);
    }

    public int CountNodes() => CountNodes(Root);
    int CountNodes(AVLNode<TValue>? node) => node == null ? 0 : 1 + CountNodes(node.Left) + CountNodes(node.Right);
    
    public int CountNodesAtLevel(int level) => CountNodesAtLevel(Root, level, 0);

    int CountNodesAtLevel(AVLNode<TValue>? node, int targetLevel, int currentLevel)
    {
        if (node == null)
            return 0;

        if (currentLevel == targetLevel)
            return 1;

        return CountNodesAtLevel(node.Left, targetLevel, currentLevel + 1) +
               CountNodesAtLevel(node.Right, targetLevel, currentLevel + 1);
    }

    public AVLNode<TValue>? FindPredecessor(TValue value) => FindPredecessor(Root, value);
    AVLNode<TValue>? FindPredecessor(AVLNode<TValue>? node, TValue value)
    {
        if (node == null) return null;

        if (Comparer<TValue>.Default.Compare(node.Value, value) >= 0)
            return FindPredecessor(node.Left, value);

        return FindPredecessor(node.Right, value) ?? node;
    }

    public AVLNode<TValue>? FindSuccessor(TValue value) => FindSuccessor(Root, value);
    AVLNode<TValue>? FindSuccessor(AVLNode<TValue>? node, TValue value)
    {
        if (node == null) 
            return null;

        if (Comparer<TValue>.Default.Compare(node.Value, value) <= 0)
            return FindSuccessor(node.Right, value);

        return FindSuccessor(node.Left, value) ?? node;
    }

    public List<TValue> PreOrderTraversal()
    {
        List<TValue> temp = new();
        PreOrderTraversal(Root, temp.Add);
        return temp;
    }
    
    void PreOrderTraversal(AVLNode<TValue>? node, Action<TValue> action)
    {
        if (node == null) return;
    
        action(node.Value);
        PreOrderTraversal(node.Left, action);
        PreOrderTraversal(node.Right, action);
    }
    
    public List<TValue> InOrderTraversal()
    {
        List<TValue> temp = new();
        InOrderTraversal(Root, temp.Add);
        return temp;
    }

    void InOrderTraversal(AVLNode<TValue>? node, Action<TValue> action)
    {
        if (node == null) return;
        
        InOrderTraversal(node.Left, action);
        action(node.Value);
        InOrderTraversal(node.Right, action);
    }
    
    public List<TValue> ReverseInOrderTraversal()
    {
        List<TValue> temp = new();
        ReverseInOrderTraversal(Root, temp.Add);
        return temp;
    }

    void ReverseInOrderTraversal(AVLNode<TValue>? node, Action<TValue> action)
    {
        if (node == null) return;
        
        ReverseInOrderTraversal(node.Right, action);
        action(node.Value);
        ReverseInOrderTraversal(node.Left, action);
    }
    
    public List<TValue> PostOrderTraversal()
    {
        List<TValue> temp = new();
        PostOrderTraversal(Root, temp.Add);
        return temp;
    }

    void PostOrderTraversal(AVLNode<TValue>? node, Action<TValue> action)
    {
        if (node == null) return;
    
        PostOrderTraversal(node.Left, action);
        PostOrderTraversal(node.Right, action);
        action(node.Value);
    }
    
    public List<TValue> LevelOrderTraversal()
    {
        List<TValue> temp = new();
        LevelOrderTraversal(temp.Add);
        return temp;
    }
    
    void LevelOrderTraversal(Action<TValue> action) //Breadth-first traversal (в ширину)
    {
        if (Root == null) return;

        Queue.Queue<AVLNode<TValue>> queue = new();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            AVLNode<TValue> node = queue.Dequeue();
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

    void ClearRecursive(AVLNode<TValue>? node)
    {
        if (node == null)
            return;

        ClearRecursive(node.Left);
        ClearRecursive(node.Right);

        node.Left = null;
        node.Right = null;
    }
    
    public AVLTree<TValue> Clone()
    {
        AVLTree<TValue> clonedTree = new();

        if (Root != null)
        {
            clonedTree.Root = CloneNode(Root);
            clonedTree.Count = Count;
        }

        return clonedTree;
    }

    AVLNode<TValue>? CloneNode(AVLNode<TValue>? node)
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