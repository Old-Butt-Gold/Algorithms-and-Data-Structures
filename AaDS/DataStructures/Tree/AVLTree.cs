//Аналог TreeSet на AVLTree

using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Tree;

public class AVLTree<TValue>
{
    AVLNode<TValue>? _root;

    public int Count { get; set; }

    public bool IsEmpty => Count == 0;

    public AVLTree() { }
    
    public AVLTree(IEnumerable<TValue>? collection)
    {
        if (collection != null)
        {
            foreach (var item in collection)
            {
                Add(item);
                Count++;
            }
        }
    }
    
    AVLNode<TValue>? RotateRight(AVLNode<TValue>? node)
    {
        var newRoot = node!.Left;
        node!.Left = newRoot!.Right;
        newRoot.Right = node;
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        newRoot.Height = 1 + Math.Max(Height(newRoot.Left), Height(newRoot.Right));
        return newRoot;
    }

    AVLNode<TValue>? RotateLeft(AVLNode<TValue>? node)
    {
        var newRoot = node!.Right;
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
            _root = Insert(_root, value);
            Count++;
            return true;
        }

        return false;
        
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
    }
    
    public bool Remove(TValue value)
    {
        if (Contains(value))
        {
            _root = Delete(_root, value);
            Count--;
            return true;
        }

        return false;
        
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
            
                node.Value = FindMinNode(node.Right)!.Value;
                node.Right = Delete(node.Right, node.Value);
            }

            return Balance(node);
        }
        
        AVLNode<TValue>? FindMinNode(AVLNode<TValue>? node) => node?.Left == null ? node : FindMinNode(node.Left);
    }
    
    public TValue FindMinValue()
    {
        return FindMinValueRecursive(_root);
        
        TValue FindMinValueRecursive(AVLNode<TValue>? node) => node?.Left == null ? node!.Value : FindMinValueRecursive(node.Left);
    }
    
    public TValue FindMaxValue()
    {
        return FindMaxValueRecursive(_root);
        
        TValue FindMaxValueRecursive(AVLNode<TValue>? node) => node!.Right == null ? node.Value : FindMaxValueRecursive(node.Right);
    }
    
    public bool RemoveMin()
    {
        if (_root is null)
            return false;
        _root = RemoveMinNode(_root);
        return true;
        
        AVLNode<TValue>? RemoveMinNode(AVLNode<TValue>? node)
        {
            if (node?.Left == null)
                return node?.Right;

            node.Left = RemoveMinNode(node.Left);
            return Balance(node);
        }
    }
    
    public bool RemoveMax()
    {
        if (_root == null)
            return false;

        _root = RemoveMaxNode(_root);
        return true;
        
        AVLNode<TValue>? RemoveMaxNode(AVLNode<TValue>? node)
        {
            if (node?.Right == null)
                return node?.Left;

            node.Right = RemoveMaxNode(node.Right);
            return Balance(node);
        }
    }

    public bool Contains(TValue value)
    {
        return Search(_root, value);
        
        bool Search(AVLNode<TValue>? node, TValue value)
        {
            if (node == null)
                return false;
            int comparison = Comparer<TValue>.Default.Compare(value, node.Value);
            if (comparison == 0)
                return true;
            return Search(comparison < 0 ? node.Left : node.Right, value);
        }
    }
    
    public int TreeHeight() => Height(_root);
    
    public List<TValue> PreOrderTraversal()
    {
        List<TValue> temp = [];
        PreOrderTraversal(_root, temp);
        return temp;
    }
    
    public void PreOrderTraversal(IList<TValue> list)
    {
        list.Clear();
        PreOrderTraversal(_root, list);
    }

    public void PreOrderTraversal(AVLNode<TValue>? node, IList<TValue> list)
    {
        if (node == null) return;
    
        list.Add(node.Value);
        PreOrderTraversal(node.Left, list);
        PreOrderTraversal(node.Right, list);
    }
    
    public List<TValue> InOrderTraversal()
    {
        List<TValue> temp = [];
        InOrderTraversal(_root, temp);
        return temp;
    }
    
    public void InOrderTraversal(IList<TValue> list)
    {
        list.Clear();
        InOrderTraversal(_root, list);
    }

    void InOrderTraversal(AVLNode<TValue>? node, IList<TValue> list)
    {
        if (node == null) return;
        
        InOrderTraversal(node.Left, list);
        list.Add(node.Value);
        InOrderTraversal(node.Right, list);
    }
    
    public List<TValue> ReverseInOrderTraversal()
    {
        List<TValue> temp = [];
        ReverseInOrderTraversal(_root, temp);
        return temp;
    }
    
    public void ReverseInOrderTraversal(IList<TValue> list)
    {
        list.Clear();
        ReverseInOrderTraversal(_root, list);
    }

    void ReverseInOrderTraversal(AVLNode<TValue>? node, IList<TValue> list)
    {
        if (node == null) return;
        
        ReverseInOrderTraversal(node.Right, list);
        list.Add(node.Value);
        ReverseInOrderTraversal(node.Left, list);
    }
    
    public List<TValue> PostOrderTraversal()
    {
        List<TValue> temp = [];
        PostOrderTraversal(_root, temp);
        return temp;
    }
    
    public void PostOrderTraversal(IList<TValue> list)
    {
        list.Clear();
        PostOrderTraversal(_root, list);
    }

    void PostOrderTraversal(AVLNode<TValue>? node, IList<TValue> list)
    {
        if (node == null) return;
    
        PostOrderTraversal(node.Left, list);
        PostOrderTraversal(node.Right, list);
        list.Add(node.Value);
    }
    
    public List<TValue> LevelOrderTraversal()
    {
        List<TValue> temp = [];
        LevelOrderTraversal(temp);
        return temp;
    }
    
    void LevelOrderTraversal(IList<TValue> list) //Breadth-first traversal (в ширину)
    {
        list.Clear();
        if (_root == null) return;

        Queue.Queue<AVLNode<TValue>> queue = new();
        queue.Enqueue(_root);

        while (queue.Count > 0)
        {
            AVLNode<TValue> node = queue.Dequeue();
            list.Add(node.Value);

            if (node.Left != null)
                queue.Enqueue(node.Left);

            if (node.Right != null)
                queue.Enqueue(node.Right);
        }
    }
    
    public void Clear()
    {
        ClearRecursive(_root);
        _root = null;
        Count = 0;
        
        void ClearRecursive(AVLNode<TValue>? node)
        {
            if (node == null)
                return;

            ClearRecursive(node.Left);
            ClearRecursive(node.Right);

            node.Left = null;
            node.Right = null;
        }
    }
    
    public AVLTree<TValue> Clone()
    {
        AVLTree<TValue> clonedTree = new();

        if (_root != null)
        {
            clonedTree._root = CloneNode(_root);
            clonedTree.Count = Count;
        }

        return clonedTree;
        
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
}