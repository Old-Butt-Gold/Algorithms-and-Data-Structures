using System.Collections;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Tree;

class BinarySearchTree<TValue> : IEnumerable<TValue> where TValue : IComparable<TValue>
{
    public int Count { get; set; }
    public bool IsEmpty => Count == 0;
    BSTNode<TValue>? Root;
    public BinarySearchTree() { }

    public BinarySearchTree(TValue value) => (Root, Count) = (new(value), 1);

    public BinarySearchTree(System.Collections.Generic.IEnumerable<TValue>? collection)
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
        
        BSTNode<TValue>? FindMinNode(BSTNode<TValue>? node) => node?.Left == null ? node : FindMinNode(node.Left);
    }
    
    public TValue FindMinValue()
    {
        return FindMinValueRecursive(Root);
        
        TValue FindMinValueRecursive(BSTNode<TValue>? node) => node.Left == null ? node.Value : FindMinValueRecursive(node.Left);
    }
    
    public TValue FindMaxValue()
    {
        return FindMaxValueRecursive(Root);
        
        TValue FindMaxValueRecursive(BSTNode<TValue>? node) => node.Right == null ? node.Value : FindMaxValueRecursive(node.Right);
    }
    
    public int CountIncompleteNodes()
    {
        return Count(Root);
        
        int Count(BSTNode<TValue> bstNode)
        {
            if (Root is null) 
                return 0;

            int count = 0;

            if ((Root.Left is null && Root.Right != null) || (Root.Right is null && Root.Left != null))
            {
                count++;
            }

            count += Count(Root.Left);
            count += Count(Root.Right);

            return count;
        }
    }
    
    public bool RemoveMin()
    {
        if (Root == null)
            return false;

        Root = RemoveMinNode(Root);
        Count--;
        return true;
        
        BSTNode<TValue>? RemoveMinNode(BSTNode<TValue>? node)
        {
            if (node?.Left == null)
                return node?.Right;

            node.Left = RemoveMinNode(node.Left);
            return node;
        }
    }
    
    public bool RemoveMax()
    {
        if (Root == null)
            return false;

        Root = RemoveMaxNode(Root);
        Count--;
        return true;
        
        BSTNode<TValue>? RemoveMaxNode(BSTNode<TValue>? node)
        {
            if (node?.Right == null)
                return node?.Left;

            node.Right = RemoveMaxNode(node.Right);
            return node;
        }
    }
    
    public int GetHeight()
    {
        return Height(Root);
        
        int Height(BSTNode<TValue>? node) => 
            node == null ? 0 : Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }
    
    public bool IsBalanced()
    {
        return CheckHeight(Root) != -1;
        
        int CheckHeight(BSTNode<TValue>? node)
        {
            if (node == null)
                return 0;

            int leftHeight = CheckHeight(node.Left);
            if (leftHeight == -1) return -1;

            int rightHeight = CheckHeight(node.Right);
            if (rightHeight == -1) return -1;

            if (Math.Abs(leftHeight - rightHeight) > 1)
                return -1;

            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }
    
    public int CountLeaves()
    {
        return Count(Root);
        
        int Count(BSTNode<TValue>? node)
        {
            if (node == null) return 0;
    
            if (node.Left == null && node.Right == null)
                return 1;
    
            return Count(node.Left) + Count(node.Right);
        }
    }
    
    public int CountNodesAtLevel(int level)
    {
        return Count(Root, level, 0);
        
        int Count(BSTNode<TValue>? node, int targetLevel, int currentLevel)
        {
            if (node == null)
                return 0;

            if (currentLevel == targetLevel)
                return 1;

            return Count(node.Left, targetLevel, currentLevel + 1) +
                   Count(node.Right, targetLevel, currentLevel + 1);
        }
    }
    
    public List<TValue> PreOrderTraversal()
    {
        List<TValue> list = new();
        Traversal(Root, list.Add);
        return list;
        
        void Traversal(BSTNode<TValue>? node, Action<TValue> action)
        {
            if (node == null) return;
    
            action(node.Value);
            Traversal(node.Left, action);
            Traversal(node.Right, action);
        }
    }
    
    public List<TValue> InOrderTraversal()
    {
        List<TValue> list = new();
        Traversal(Root, list.Add);
        return list;
        
        void Traversal(BSTNode<TValue>? node, Action<TValue> action)
        {
            if (node == null) return;
            Traversal(node.Left, action);
            action(node.Value);
            Traversal(node.Right, action);
        }
    }

    public List<TValue> ReverseInOrderTraversal()
    {
        List<TValue> list = new();
        Traversal(Root, list.Add);
        return list;
        
        void Traversal(BSTNode<TValue>? node, Action<TValue> action)
        {
            if (node == null) return;
        
            Traversal(node.Right, action);
            action(node.Value);
            Traversal(node.Left, action);
        }
    }
    
    public List<TValue> PostOrderTraversal()
    {
        List<TValue> list = new();
        Traversal(Root, list.Add);
        return list;
        
        void Traversal(BSTNode<TValue>? node, Action<TValue> action)
        {
            if (node == null) return;
    
            Traversal(node.Left, action);
            Traversal(node.Right, action);
            action(node.Value);
        }
    }
    
    public List<TValue> LevelOrderTraversal()
    {
        List<TValue> list = [];
        Traversal(list.Add);
        return list;
        
        void Traversal(Action<TValue> action) //Breadth-first traversal (в ширину)
        {
            if (Root == null) return;

            Queue<BSTNode<TValue>> queue = new();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                action(node.Value);

                if (node.Left != null)
                    queue.Enqueue(node.Left);

                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
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

    public IEnumerator<TValue> GetEnumerator() => new BSTIterator<TValue>(Root);

    class BSTIterator<TValue> : IEnumerator<TValue>
    {
        Stack<BSTNode<TValue>> _stack = new(); //второй способ как стандартный, с очередью
        BSTNode<TValue> _root;
        
        public BSTIterator(BSTNode<TValue> root)
        {
            _root = root;
            StackAllLeft(root);
        }
        
        void StackAllLeft(BSTNode<TValue>? node) {
            while (node != null) {
                _stack.Push(node);
                node = node.Left!;
            }
        }
        
        public bool MoveNext()
        {
            if (_stack.Count > 0)
            {
                var node = _stack.Pop();
                Current = node.Value;
                StackAllLeft(node.Right);

                return true;
            }

            return false;
        }

        public void Reset()
        {
            _stack.Clear();
            StackAllLeft(_root); 
        }

        public TValue Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }
    } 

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    #region OtherAlgos
    
    void Invert() => Invert(Root);

    void Invert(BSTNode<TValue>? node)
    {
        if (node == null)
            return;

        (node.Left, node.Right) = (node.Right, node.Left);

        Invert(node.Left);
        Invert(node.Right);
    }
    
    bool IsSymmetric(BSTNode<TValue> root)
    {
        return Dfs(root.Left, root.Right);
        
        bool Dfs(BSTNode<TValue>? root1, BSTNode<TValue>? root2) {
            if (root1 is null && root2 is null) 
                return true;
            
            if (root1 == null || root2 == null)
                return false;
            
            if (!EqualityComparer<TValue>.Default.Equals(root1.Value, root2.Value))
                return false;

            return Dfs(root1?.Left, root2?.Right) && Dfs(root1?.Right, root2?.Left);
        }
    }
    
    int MaxDepth(BSTNode<TValue>? root) {
        if (root is null) return 0;

        int left = MaxDepth(root.Left);
        int right = MaxDepth(root.Right);
        return Math.Max(left, right) + 1;
    }
    
    bool IsSameTree(BSTNode<TValue>? p, BSTNode<TValue>? q) {
        if (p is null && q is null) return true;
        if (p is null || q is null) return false;
        
        return EqualityComparer<TValue>.Default.Equals(p.Value, q.Value) 
               && IsSameTree(p.Left, q.Left) && IsSameTree(p.Right, q.Right);
    }

    /// <summary>
    /// Given the root of a binary tree and an integer targetSum, return true if the tree has a root-to-leaf path such that adding up all the values along the path equals targetSum.
    /// A leaf is a node with no children.
    /// </summary>
    /// <param name="root"></param>
    /// <param name="targetSum"></param>
    /// <returns></returns>
    public bool HasPathSum(BSTNode<int>? root, int targetSum)
    {
        if (root is null) return false;

        int currentSum = targetSum - root.Value;
        if (currentSum == 0 && root.Left == null && root.Right == null) return true;

        return HasPathSum(root.Left, currentSum) || HasPathSum(root.Right, currentSum);
    }
    
    public int CountNodes() => CountNodes(Root); //Count overall amount of nodes
    int CountNodes(BSTNode<TValue>? node) => node == null ? 0 : 1 + CountNodes(node.Left) + CountNodes(node.Right);
    
    /// <summary>
    /// ищет узел, который является наибольшим значением, меньшим, чем value (предшественник).
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public BSTNode<TValue>? FindPredecessor(TValue value)
    {
        return Find(Root, value);
        
        BSTNode<TValue>? Find(BSTNode<TValue>? node, TValue value)
        {
            if (node == null) return null;

            if (node.Value.CompareTo(value) >= 0)
                return Find(node.Left, value);

            return Find(node.Right, value) ?? node;
        }
    }
    
    /// <summary>
    /// ищет узел, который является наименьшим значением, большим, чем value (преемник).
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public BSTNode<TValue>? FindSuccessor(TValue value)
    {
        return Find(Root, value);
        
        BSTNode<TValue>? Find(BSTNode<TValue>? node, TValue value)
        {
            if (node == null) return null;

            if (node.Value.CompareTo(value) <= 0)
                return Find(node.Right, value);

            return Find(node.Left, value) ?? node;
        }
    }
    
    #endregion
}