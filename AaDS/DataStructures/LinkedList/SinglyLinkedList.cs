using System.Collections;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.LinkedList;

class SinglyLinkedList<T> : IEnumerable<T>
{
    Node<T>? _head;
    Node<T>? _tail;
    public int Count { get; private set; }

    public SinglyLinkedList()
    {
    }

    public SinglyLinkedList(T data) => AddLast(data);

    public SinglyLinkedList(params IEnumerable<T>[] collections)
    {
        foreach (IEnumerable<T> collection in collections)
        foreach (T item in collection)
            AddLast(item);
    }

    public void Reverse()
    {
        if (_head == null) return;

        Node<T>? prev = null;
        var current = _head;

        _tail = _head;

        while (current != null)
        {
            var next = current.Next;
            current.Next = prev;
            prev = current;
            current = next;
        }

        _head = prev;
    }

    public void AddLast(T data)
    {
        Node<T> node = new(data);

        if (_head == null)
            _head = node;
        else
            _tail!.Next = node;
        _tail = node;
        Count++;
    }

    public void AddFirst(T data)
    {
        Node<T> node = new(data) { Next = _head };
        _head = node;
        if (Count == 0)
            _tail = _head;
        Count++;
    }

    public bool Remove(T data)
    {
        Node<T>? current = _head;
        Node<T>? previous = null;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(data, current.Data))
            {
                if (previous != null)
                {
                    previous.Next = current.Next;
                    if (current.Next == null) _tail = previous;
                }
                else
                {
                    _head = _head?.Next;
                    if (_head == null) _tail = null;
                }

                Count--;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    public void DeleteFirst()
    {
        if (_head != null)
        {
            _head = _head.Next;

            if (_head == null)
                _tail = null;
            Count--;
        }
    }

    public void DeleteLast()
    {
        if (_head == null) return;

        if (_head == _tail)
            (_head, _tail) = (null, null);
        else
        {
            Node<T>? current = _head;

            while (current?.Next != _tail)
                current = current?.Next;

            current!.Next = null;
            _tail = current;
        }

        Count--;
    }

    public bool IsEmpty => Count == 0;

    public void Clear() => (_head, _tail, Count) = (null, null, 0);

    public bool Contains(T data)
    {
        Node<T>? current = _head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(data, current.Data)) return true;
            current = current.Next;
        }

        return false;
    }

    public Node<T>? Find(T data)
    {
        Node<T>? current = _head;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(data, current.Data))
                return current;

            current = current.Next;
        }

        return null;
    }

    public bool AddBefore(T existingData, T newData)
    {
        Node<T>? current = _head;
        Node<T>? previous = null;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(existingData, current.Data))
            {
                Node<T> newNode = new(newData) { Next = current };

                if (previous != null)
                    previous.Next = newNode;
                else
                    _head = newNode;

                Count++;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    public bool AddAfter(T existingData, T newData)
    {
        Node<T>? current = _head;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(existingData, current.Data))
            {
                Node<T> newNode = new(newData) { Next = current.Next };
                current.Next = newNode;

                if (current == _tail)
                    _tail = newNode;

                Count++;
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public bool AddBefore(Node<T> existingNode, T newData)
    {
        if (existingNode == null)
            throw new ArgumentNullException(nameof(existingNode), "Existing node cannot be null.");

        Node<T>? current = _head;
        Node<T>? previous = null;

        while (current != null)
        {
            if (ReferenceEquals(existingNode, current))
            {
                Node<T> newNode = new(newData) { Next = current };

                if (previous != null)
                    previous.Next = newNode;
                else
                    _head = newNode;

                Count++;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    public bool AddAfter(Node<T> existingNode, T newData)
    {
        if (existingNode == null)
            throw new ArgumentNullException(nameof(existingNode), "Existing node cannot be null.");

        Node<T>? current = _head;

        while (current != null)
        {
            if (ReferenceEquals(existingNode, current))
            {
                Node<T> newNode = new(newData) { Next = current.Next };
                current.Next = newNode;

                if (current == _tail)
                    _tail = newNode;

                Count++;
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? current = _head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    public T[] ToArray()
    {
        T[] array = new T[Count];
        int index = 0;

        foreach (T item in this)
            array[index++] = item;

        return array;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #region OtherAlgos

    Node<T> MergeTwoSortedLists(Node<T> list1, Node<T> list2)
    {
        Node<T> dummyHead = new();
        var current = dummyHead;

        while (list1 != null && list2 != null)
        {
            if (Comparer<T>.Default.Compare(list1.Data, list2.Data) < 0)
            {
                current.Next = list1;
                list1 = list1.Next!;
            }
            else
            {
                current.Next = list2;
                list2 = list2.Next!;
            }

            current = current.Next;
        }

        current.Next = list1 != null ? list1 : list2;

        return dummyHead.Next!;
    }

    /// <summary>
    /// You are given an array of k linked-lists lists, each linked-list is sorted in ascending order.
    /// Merge all the linked-lists into one sorted linked-list and return it.
    /// </summary>
    /// <param name="lists"></param>
    /// <returns></returns>
    Node<T>? MergeKListsQueue(Node<T>?[]? lists)
    {
        if (lists == null || lists.Length == 0)
            return null;
        if (lists.Length == 1)
            return lists[0];

        PriorityQueue<Node<T>, T> result = new();
        foreach (var list in lists)
        {
            if (list != null)
            {
                result.Enqueue(list, list.Data);
            }
        }

        Node<T> root = new();
        var current = root;
        while (result.Count > 0)
        {
            var node = result.Dequeue();
            if (node.Next != null)
            {
                result.Enqueue(node.Next, node.Next.Data);
            }

            current.Next = node;
            current = current.Next;
        }

        return root.Next;
    }

    /// <summary>
    /// You are given an array of k linked-lists lists, each linked-list is sorted in ascending order.
    /// Merge all the linked-lists into one sorted linked-list and return it.
    /// </summary>
    /// <param name="lists"></param>
    /// <returns></returns>
    Node<T>? MergeKListsDivideAndConqueror(Node<T>?[]? lists)
    {
        if (lists == null || lists.Length == 0)
            return null;
        if (lists.Length == 1)
            return lists[0];


        var interval = 1;
        var length = lists.Length;

        while (interval < length)
        {
            for (int i = 0; i < length - interval; i += interval * 2)
            {
                lists[i] = MergeTwoSortedLists(lists[i], lists[i + interval]);
            }

            interval *= 2;
        }

        return lists[0];
    }

    bool HasCycle(Node<T> head)
    {
        var slow = head;
        var fast = head;
        while (slow != null && fast != null)
        {
            slow = slow.Next;

            fast = fast.Next;
            if (fast != null)
            {
                fast = fast.Next;
            }
            else
            {
                return false;
            }

            if (slow == fast) return true;
        }

        return false;
    }

    Node<T>? RotateRight(Node<T> head, int k)
    {
        if (head?.Next == null || k == 0)
        {
            return head;
        }

        var tail = head;
        int count = 1;
        while (tail.Next != null)
        {
            tail = tail.Next;
            count++;
        }

        k %= count;
        if (k is 0) return head;
        tail.Next = head;

        for (int i = 0; i < count - k; i++)
        {
            head = head.Next!;
            tail = tail!.Next;
        }

        tail!.Next = null;

        return head;
    }

    Node<T>? RemoveNthFromEnd(Node<T>? head, int n)
    {
        var slow = head;
        var fast = head;

        for (int i = 0; i < n; i++)
        {
            fast = fast?.Next;
        }

        if (fast == null)
        {
            return head?.Next;
        }

        while (fast.Next != null)
        {
            slow = slow?.Next;
            fast = fast.Next;
        }

        slow!.Next = slow.Next?.Next;

        return head;
    }

    Node<T> DeleteDuplicates(Node<T> head) //only in sorted
    {
        if (head is null) return head;
        var sentinel = new Node<T>
        {
            Next = head
        };
        var prev = sentinel;
        while (head != null)
        {
            if (head.Next != null && EqualityComparer<T>.Default.Equals(head.Data, head.Next.Data))
            {
                while (head.Next != null && EqualityComparer<T>.Default.Equals(head.Data, head.Next.Data))
                {
                    head = head.Next;
                }

                prev!.Next = head.Next;
            }
            else
            {
                prev = prev!.Next;
            }

            head = head.Next!;
        }

        return sentinel.Next;
    }

    class NodeWithRandom
    {
        public int val;
        public NodeWithRandom next;
        public NodeWithRandom random;

        public NodeWithRandom(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/copy-list-with-random-pointer/?envType=study-plan-v2&envId=top-interview-150
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    NodeWithRandom? CopyRandomListDictionary(NodeWithRandom? head) //O(n) memory, O(n) time complexity
    {
        if (head is null) return head;

        var oldToNew = new Dictionary<NodeWithRandom, NodeWithRandom>();

        var curr = head;
        while (curr != null)
        {
            oldToNew[curr] = new(curr.val);
            curr = curr.next;
        }

        curr = head;
        while (curr != null)
        {
            oldToNew[curr].next = curr.next != null ? oldToNew[curr.next] : null;
            oldToNew[curr].random = curr.random != null ? oldToNew[curr.random] : null;
            curr = curr.next;
        }

        return oldToNew[head];
    }

    /// <summary>
    /// Given the head of a linked list and a value x,
    /// partition it such that all nodes less than x come before nodes greater than or equal to x.
    /// You should preserve the original relative order of the nodes in each of the two partitions.
    /// </summary>
    /// <param name="head"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    Node<int> Partition(Node<int> head, int x)
    {
        Node<int> before = new();
        Node<int> after = new();
        var beforeCurr = before;
        var afterCurr = after;

        while (head != null)
        {
            if (head.Data < x)
            {
                beforeCurr.Next = head;
                beforeCurr = beforeCurr.Next;
            }
            else
            {
                afterCurr.Next = head;
                afterCurr = afterCurr.Next;
            }

            head = head.Next;
        }

        afterCurr.Next = null;
        beforeCurr.Next = after.Next;
        return before.Next;
    }

    /// <summary>
    /// Given the head of a singly linked list and two integers left and right where left <= right,
    /// reverse the nodes of the list from position left to position right, and return the reversed list.
    /// </summary>
    /// <param name="head"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    Node<int>? ReverseBetween(Node<int>? head, int left, int right)
    {
        if (head is null || left == right) return head;

        Node<int> dummy = new()
        {
            Next = head
        };
        var prev = dummy;

        for (int i = 0; i < left - 1; i++)
        {
            prev = prev.Next;
        }

        var current = prev.Next;

        for (int i = 0; i < right - left; i++)
        {
            var nextNode = current.Next;
            current.Next = nextNode.Next;
            nextNode.Next = prev.Next;
            prev.Next = nextNode;
        }

        return dummy.Next;
    }

    /// <summary>
    /// You are given two non-empty linked lists representing two non-negative integers. The digits are stored in reverse order, and each of their nodes contains a single digit. Add the two numbers and return the sum as a linked list.
    /// You may assume the two numbers do not contain any leading zero, except the number 0 itself.
    /// </summary>
    /// <param name="l1"></param>
    /// <param name="l2"></param>
    /// <param name="carry"></param>
    /// <returns></returns>
    Node<int>? AddTwoNumbers(Node<int>? l1, Node<int>? l2, int carry = 0)
    {
        if (l1 == null && l2 == null && carry == 0)
            return null;

        var total = (l1?.Data ?? 0) + (l2?.Data ?? 0) + carry;
        carry = total / 10;
        return new(total % 10)
        {
            Next = AddTwoNumbers(l1?.Next, l2?.Next, carry)
        };
    }

    /// <summary>
    /// You are given two non-empty linked lists representing two non-negative integers. The most significant digit comes first and each of their nodes contains a single digit. Add the two numbers and return the sum as a linked list.
    /// You may assume the two numbers do not contain any leading zero, except the number 0 itself.
    /// </summary>
    /// <param name="l1"></param>
    /// <param name="l2"></param>
    /// <returns></returns>
    Node<int>? AddTwoNumbers(Node<int>? l1, Node<int>? l2)
    {
        Stack<int> stack1 = [];
        Stack<int> stack2 = [];
        while (l1 != null)
        {
            stack1.Push(l1.Data);
            l1 = l1.Next;
        }

        while (l2 != null)
        {
            stack2.Push(l2.Data);
            l2 = l2.Next;
        }

        Node<int>? result = null;
        int carry = 0;

        while (stack1.Count > 0 || stack2.Count > 0 || carry != 0)
        {
            var digit1 = stack1.Count > 0 ? stack1.Pop() : 0;
            var digit2 = stack2.Count > 0 ? stack2.Pop() : 0;

            int sum = digit1 + digit2 + carry;
            carry = sum / 10;

            var newNode = new Node<int>(sum % 10)
            {
                Next = result
            };

            result = newNode;
        }

        return result;
    }

    /// <summary>
    /// Given the head of a linked list, return the list after sorting it in ascending order.
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    Node<int>? SortList(Node<int>? head)
    {
        if (head?.Next == null)
        {
            return head;
        }

        var middle = GetMiddle(head);
        var nextToMiddle = middle!.Next;
        middle.Next = null;

        var left = SortList(head);
        var right = SortList(nextToMiddle);

        var sortedList = SortedMerge(left, right);
        return sortedList;

        Node<int>? GetMiddle(Node<int>? head)
        {
            if (head == null)
            {
                return head;
            }

            var slow = head;
            var fast = head;

            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow!.Next;
                fast = fast.Next.Next;
            }

            return slow;
        }

        Node<int>? SortedMerge(Node<int>? left, Node<int>? right)
        {
            if (left is null)
            {
                return right;
            }

            if (right is null)
            {
                return left;
            }

            Node<int> dummy = new();
            var temp = dummy;

            while (left != null && right != null)
            {
                if (left.Data <= right.Data)
                {
                    temp.Next = left;
                    left = left.Next;
                }
                else
                {
                    temp.Next = right;
                    right = right.Next;
                }

                temp = temp.Next;
            }

            temp.Next = left != null ? left : right;

            return dummy.Next;
        }
    }

    /// <summary>
    /// Given the head of a linked list, reverse the nodes of the list k at a time, and return the modified list.
    /// <para>
    /// k is a positive integer and is less than or equal to the length of the linked list. If the number of nodes is not a multiple of k then left-out nodes, in the end, should remain as it is.
    /// </para>
    /// <para>
    /// You may not alter the values in the list's nodes, only nodes themselves may be changed.
    /// </para>
    /// </summary>
    /// <param name="head"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public Node<int>? ReverseKGroup(Node<int>? head, int k)
    {
        var current = head;
        int count = 0;
        //find the k + 1 node
        while (current != null && count != k)
        {
            current = current.Next;
            count++;
        }

        if (count == k)
        {
            current = ReverseKGroup(current, k); //reverse list with k + 1 node as head


            while (count-- > 0)
            {
                var tmp = head!.Next;
                head.Next = current;
                current = head;
                head = tmp;
            }

            head = current;
        }

        return head;
    }

    #endregion
}

