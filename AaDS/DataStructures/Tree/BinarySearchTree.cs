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
        List<TValue> temp = new();
        PreOrderTraversal(Root, temp);
        return temp;
    }
    
    public void PreOrderTraversal(IList<TValue> list)
    {
        list.Clear();
        PreOrderTraversal(Root, list);
    }

    public void PreOrderTraversal(BSTNode<TValue>? node, IList<TValue> list)
    {
        if (node == null) return;
    
        list.Add(node.Value);
        PreOrderTraversal(node.Left, list);
        PreOrderTraversal(node.Right, list);
    }
    
    public List<TValue> InOrderTraversal()
    {
        List<TValue> temp = new();
        InOrderTraversal(Root, temp);
        return temp;
    }
    
    public void InOrderTraversal(IList<TValue> list)
    {
        list.Clear();
        InOrderTraversal(Root, list);
    }

    void InOrderTraversal(BSTNode<TValue>? node, IList<TValue> list)
    {
        if (node == null) return;
        
        InOrderTraversal(node.Left, list);
        list.Add(node.Value);
        InOrderTraversal(node.Right, list);
    }
    
    public List<TValue> ReverseInOrderTraversal()
    {
        List<TValue> temp = new();
        ReverseInOrderTraversal(Root, temp);
        return temp;
    }
    
    public void ReverseInOrderTraversal(IList<TValue> list)
    {
        list.Clear();
        ReverseInOrderTraversal(Root, list);
    }

    void ReverseInOrderTraversal(BSTNode<TValue>? node, IList<TValue> list)
    {
        if (node == null) return;
        
        ReverseInOrderTraversal(node.Right, list);
        list.Add(node.Value);
        ReverseInOrderTraversal(node.Left, list);
    }
    
    public List<TValue> PostOrderTraversal()
    {
        List<TValue> temp = new();
        PostOrderTraversal(Root, temp);
        return temp;
    }
    
    public void PostOrderTraversal(IList<TValue> list)
    {
        list.Clear();
        PostOrderTraversal(Root, list);
    }

    void PostOrderTraversal(BSTNode<TValue>? node, IList<TValue> list)
    {
        if (node == null) return;
    
        PostOrderTraversal(node.Left, list);
        PostOrderTraversal(node.Right, list);
        list.Add(node.Value);
    }
    
    public List<TValue> LevelOrderTraversal()
    {
        List<TValue> temp = new();
        LevelOrderTraversal(temp);
        return temp;
    }
    
    void LevelOrderTraversal(IList<TValue> list) //Breadth-first traversal (в ширину)
    {
        list.Clear();
        if (Root == null) return;

        Queue.Queue<BSTNode<TValue>> queue = new();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            list.Add(node.Value);

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
    
    public BinarySearchTree<TValue> Clone()
    {
        BinarySearchTree<TValue> clonedTree = new();

        if (Root != null)
        {
            clonedTree.Root = CloneNode(Root);
            clonedTree.Count = Count;
        }

        return clonedTree;
        
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
    
    /// <summary>
    /// Invert the binary search tree by swapping left and right children recursively.
    /// </summary>
    void Invert()
    {
        InvertTree(Root);
        
        void InvertTree(BSTNode<TValue>? node)
        {
            if (node == null)
                return;

            (node.Left, node.Right) = (node.Right, node.Left);

            InvertTree(node.Left);
            InvertTree(node.Right);
        }
    }
    
    /// <summary>
    /// Checks if the binary tree is symmetric around its center.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Computes the maximum depth (height) of the binary tree.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    int MaxDepth(BSTNode<TValue>? root) {
        if (root is null) return 0;

        int left = MaxDepth(root.Left);
        int right = MaxDepth(root.Right);
        return Math.Max(left, right) + 1;
    }
    
    /// <summary>
    /// Determines if two binary trees are identical.
    /// </summary>
    /// <param name="p"></param>
    /// <param name="q"></param>
    /// <returns></returns>
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
    bool HasPathSum(BSTNode<int>? root, int targetSum)
    {
        if (root is null) return false;

        int currentSum = targetSum - root.Value;
        if (currentSum == 0 && root.Left == null && root.Right == null) return true;

        return HasPathSum(root.Left, currentSum) || HasPathSum(root.Right, currentSum);
    }
    
    /// <summary>
    /// Counts the total number of nodes in the binary tree.
    /// </summary>
    /// <returns></returns>
    int CountNodes()
    {
        return Count(Root);
        //Count overall amount of nodes
        
        int Count(BSTNode<TValue>? node) => node == null ? 0 : 1 + Count(node.Left) + Count(node.Right);
    }
    
    /// <summary>
    /// ищет узел, который является наибольшим значением, меньшим, чем value (предшественник).
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    BSTNode<TValue>? FindPredecessor(TValue value)
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
    BSTNode<TValue>? FindSuccessor(TValue value)
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
    
    /// <summary>
    /// Given the root of a binary tree, flatten the tree into a "linked list" (right-order).
    /// The "linked list" should be in the same order as a pre-order traversal of the binary tree.
    /// </summary>
    /// <param name="root"></param>
    void Flatten(BSTNode<TValue> root)
    {
        if (root is null) return;
        
        Flatten(root.Left);
        Flatten(root.Right);

        if (root.Left != null)
        {
            var right = root.Right;
            root.Right = root.Left;
            root.Left = null;
            var last = root;
            while (last.Right != null)
            {
                last = last.Right;
            }

            last.Right = right;
        }
    }
    
    /// <summary>
    /// Populate each next pointer to point to its next right node. If there is no next right node, the next pointer should be set to NULL.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    NodeNext? Connect(NodeNext? root)
    {
        if (root is null) return root;
        Queue<NodeNext> queue = new();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            int count = queue.Count;
            NodeNext? prev = null;
            for (int i = 0; i < count; i++)
            {
                var current = queue.Dequeue();
                if (current.right != null) queue.Enqueue(current.right);
                if (current.left != null) queue.Enqueue(current.left);
                current.next = prev;
                prev = current;
            }
        }

        return root;
    }
    
    class NodeNext(int val = 0, NodeNext left = null, NodeNext right = null, NodeNext next = null)
    {
        public int val = val;
        public NodeNext left = left;
        public NodeNext right = right;
        public NodeNext next = next;
    }
    
    /// <summary>
    /// Given the root of a binary tree, imagine yourself standing on the right side of it,
    /// return the values of the nodes you can see ordered from top to bottom.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    IList<TValue> RightSideView(BSTNode<TValue> root) {
        if (root is null) return new List<TValue>();

        List<TValue> result = [];
        Queue<BSTNode<TValue>> queue = new();
        queue.Enqueue(root);

        while (queue.Count > 0) {
            int count = queue.Count;
            for (int i = 0; i < count; i++) {
                var node = queue.Dequeue();
                if (node.Left != null) {
                    queue.Enqueue(node.Left);
                }
                if (node.Right != null) {
                    queue.Enqueue(node.Right);
                }
                if (i == count - 1) {
                    result.Add(node.Value);
                }
            }
        }

        return result;
    }
    
    /// <summary>
    /// Given the root of a binary tree, return the level order traversal of its nodes' values. (i.e., from left to right, level by level).
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    IList<IList<TValue>> LevelOrder(BSTNode<TValue> root) {
        if (root is null) return new List<IList<TValue>>();
        
        List<IList<TValue>> result = [];
        Queue<BSTNode<TValue>> queue = new();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            int count = queue.Count;
            List<TValue> temp = [];
            for (int i = 0; i < count; i++)
            {
                var node = queue.Dequeue();
                temp.Add(node.Value);
                
                if (node.Left != null)
                {
                    queue.Enqueue(node.Left);    
                }

                if (node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
            }
            result.Add(temp);
        }

        return result;
    }

    /// <summary>
    /// Given the root of a binary tree, return the zigzag level order traversal of its nodes' values.
    /// (i.e., from left to right then right to left for the next level and alternate between).
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    IList<IList<TValue>> ZigzagLevelOrder(BSTNode<TValue>? root)
    {
        var result = new List<IList<TValue>>();
        if (root == null) return result;

        var queue = new Queue<BSTNode<TValue>>();
        queue.Enqueue(root);
        bool leftToRight = true;

        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            var currentLevel = new TValue[levelSize];

            for (int i = 0; i < levelSize; i++)
            {
                var node = queue.Dequeue();
                int index = leftToRight ? i : levelSize - 1 - i;
                currentLevel[index] = node.Value;

                if (node.Left != null)
                {
                    queue.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
            }

            result.Add(currentLevel);
            leftToRight = !leftToRight;
        }

        return result;
    }
    
    /// <summary>
    /// Given the root of a binary tree, return the average value of the nodes on each level in the form of an array.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    IList<double> AverageOfLevels(BSTNode<int> root) {
        var result = new List<double>();
        if (root == null) return result;

        var queue = new Queue<BSTNode<int>>();
        queue.Enqueue(root);
        bool leftToRight = true;

        while (queue.Count > 0)
        {
            double temp = 0;
            int levelSize = queue.Count;

            for (int i = 0; i < levelSize; i++)
            {
                var node = queue.Dequeue();
                temp += node.Value;
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }

            temp /= levelSize;
            result.Add(Math.Round(temp, 5));
        }

        return result;
    }

    /// <summary>
    /// Given a binary tree, find the lowest common ancestor (LCA) of two given nodes in the tree.
    /// According to the definition of LCA on Wikipedia: “The lowest common ancestor is defined between two nodes p and q as the lowest node in T
    /// that has both p and q as descendants (where we allow a node to be a descendant of itself).”
    /// </summary>
    /// <param name="root"></param>
    /// <param name="p"></param>
    /// <param name="q"></param>
    /// <returns></returns>
    BSTNode<TValue>? LowestCommonAncestor(BSTNode<TValue>? root, BSTNode<TValue> p, BSTNode<TValue> q)
    {
        if (root is null || root == p || root == q) return root;

        var left = LowestCommonAncestor(root.Left, p, q);
        var right = LowestCommonAncestor(root.Right, p, q);

        if (left != null && right != null)
        {
            return root;
        }

        return left ?? right;
    }

    /// <summary>
    /// A path in a binary tree is a sequence of nodes where each pair of adjacent nodes in the sequence has an edge connecting them. A node can only appear in the sequence at most once. Note that the path does not need to pass through the root.
    /// The path sum of a path is the sum of the node's values in the path.
    /// Given the root of a binary tree, return the maximum path sum of any non-empty path.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    int MaxPathSum(BSTNode<int>? root)
    {
        int maxSum = int.MinValue;
        Traverse(root);
        return maxSum;

        int Traverse(BSTNode<int>? node)
        {
            if (node == null) return 0;

            int leftSum = Math.Max(0, Traverse(node.Left));
            int rightSum = Math.Max(0, Traverse(node.Right));

            int pathSum = leftSum + rightSum + node.Value;
            maxSum = Math.Max(maxSum, pathSum);

            return Math.Max(leftSum, rightSum) + node.Value;
        }
    }

    /// <summary>
    /// Given the root of a Binary Search Tree (BST), return the minimum absolute difference between the values of any two different nodes in the tree.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    int GetMinimumDifference(BSTNode<int>? root)
    {
        int lastValue = int.MaxValue;
        int minimumDelta = int.MaxValue;
        InorderTraverse(root);
        return minimumDelta;

        void InorderTraverse(BSTNode<int>? node)
        {
            if (node is null) return;

            InorderTraverse(node.Left);
            minimumDelta = Math.Min(minimumDelta, Math.Abs(lastValue - node.Value));

            lastValue = node.Value;
            InorderTraverse(node.Right);
        }
    }

    /// <summary>
    /// Given the root of a binary search tree, and an integer k, return the kth smallest value (1-indexed) of all the values of the nodes in the tree.
    /// </summary>
    /// <param name="root"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    int KthSmallest(BSTNode<int> root, int k) {
        int counter = 0;
        int value = 0;
        InorderTraverse(root);

        return value;

        void InorderTraverse(BSTNode<int>? node) {
            if (node == null) return;

            InorderTraverse(node.Left);
            counter++;
            if (counter == k)
            {
                value = node.Value;
            }

            InorderTraverse(node.Right);
        }
    }

    /// <summary>
    /// Given the root of a binary tree, determine if it is a valid binary search tree (BST).
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    bool IsValidBst(BSTNode<int>? root)
    {
        return Traverse(root, int.MinValue, int.MaxValue);

        bool Traverse(BSTNode<int>? node, int min, int max)
        {
            if (node is null) return true;

            return node.Value > min && node.Value < max
                                  && Traverse(node.Left, min, node.Value)
                                  && Traverse(node.Right, node.Value, max);
        }
    }

    #endregion
}