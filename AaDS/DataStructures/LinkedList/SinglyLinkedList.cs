using System.Collections;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.LinkedList;

class SinglyLinkedList<T> : IEnumerable<T>
{
    Node<T>? _head; 
    Node<T>? _tail; 
    public int Count { get; private set; }

    public SinglyLinkedList() { }
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
    
    public void AppendFirst(T data)
    {
        Node<T> node = new(data) { Next = _head };
        _head = node;
        if (Count == 0)
            _tail = _head;
        Count++;
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
    
    Node<T> MergeTwoSortedLists(Node<T> list1, Node<T> list2) {
        Node<T> dummyHead = new(); 
        var current = dummyHead; 

        while (list1 != null && list2 != null) {
            if (Comparer<T>.Default.Compare(list1.Data, list2.Data) < 0) {
                current.Next = list1;
                list1 = list1.Next!;
            } else {
                current.Next = list2;
                list2 = list2.Next!;
            }
            current = current.Next;
        }

        current.Next = list1 != null ? list1 : list2;

        return dummyHead.Next!; 
    }
    
    bool HasCycle(Node<T> head) {
        var slow = head;
        var fast = head;
        while (slow != null && fast != null) {
            slow = slow.Next;
            
            fast = fast.Next;
            if (fast != null) {
                fast = fast.Next;
            } else {
                return false;
            }

            if (slow == fast) return true;
        }
        return false;
    }
    
    Node<T>? RotateRight(Node<T> head, int k) {
        if (head?.Next == null || k == 0) {
            return head;
        }

        var tail = head;
        int count = 1;
        while (tail.Next != null) {
            tail = tail.Next;
            count++;
        }

        k %= count;
        if (k is 0) return head;
        tail.Next = head;

        for (int i = 0; i < count - k; i++) {
            head = head.Next!;
            tail = tail!.Next;
        }    

        tail!.Next = null;

        return head;
    }
    
    Node<T>? RemoveNthFromEnd(Node<T>? head, int n) {
        var slow = head;
        var fast = head;

        for (int i = 0; i < n; i++) {
            fast = fast?.Next;
        }

        if (fast == null) {
            return head?.Next;
        }

        while (fast.Next != null) {
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
    
    #endregion
}

