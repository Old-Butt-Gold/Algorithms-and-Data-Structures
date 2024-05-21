using System.Collections;
using AaDS.DataStructures.Shared;

//а-ля Deque

namespace AaDS.DataStructures.LinkedList;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    DoublyNode<T>? _head;
    DoublyNode<T>? _tail;
    public int Count { get; private set; }

    public DoublyLinkedList() { }

    public DoublyLinkedList(T data) => AddLast(data);

    public DoublyLinkedList(params IEnumerable<T>[] collections)
    {
        foreach (IEnumerable<T> collection in collections)
        foreach (T item in collection)
            AddLast(item);
    }
    
    public T PeekFirst()
    { 
        if (IsEmpty) throw new InvalidOperationException(); 
        return _head!.Data;
    }
        
    public T PeekLast()
    {
        if (IsEmpty) throw new InvalidOperationException();
        return _tail!.Data;
    }
    
    public bool TryPeekFirst(out T? data)
    {
        if (IsEmpty)
        {
            data = default;
            return false;
        }

        data = _head!.Data;
        return true;
    }
        
    public bool TryPeekLast(out T? data)
    {
        if (IsEmpty)
        {
            data = default;
            return false;
        }

        data = _tail!.Data;
        return true;
    }

    public void AddLast(T data)
    {
        DoublyNode<T> node = new(data);

        if (_head == null)
            _head = node;
        else
        {
            _tail!.Next = node;
            node.Previous = _tail;
        }

        _tail = node;
        Count++;
    }

    public void AddFirst(T data)
    {
        DoublyNode<T> node = new(data) { Next = _head };
        if (Count == 0)
            _head = _tail = node;
        else
        {
            _head!.Previous = node;
            _head = node;
        }

        Count++;
    }

    public T[] ToArray()
    {
        T[] array = new T[Count];
        int index = 0;

        foreach (T item in this)
        {
            array[index++] = item;
        }

        return array;
    }
    
    public DoublyNode<T>? Find(T data)
    {
        DoublyNode<T>? current = _head;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, data))
                return current;

            current = current.Next;
        }

        return null;
    }

    public T RemoveFirst()
    {
        if (_head == null) throw new InvalidOperationException();

        T data = _head.Data;
        _head = _head.Next;

        if (_head != null)
            _head.Previous = null;
        else
            _tail = null;

        Count--;
        return data;
    }

    public T RemoveLast()
    {
        if (_tail == null) throw new InvalidOperationException();

        T data = _tail.Data;
        _tail = _tail.Previous;

        if (_tail != null)
            _tail.Next = null;
        else
            _head = null;

        Count--;
        return data;
    }
    
    public void AddBefore(DoublyNode<T> existingNode, T newData)
    {
        if (existingNode == null)
            throw new ArgumentNullException(nameof(existingNode), "Existing node cannot be null.");

        if (!NodeBelongsToList(existingNode))
            throw new InvalidOperationException("The provided node does not belong to this list.");

        DoublyNode<T> newNode = new(newData) { Next = existingNode, Previous = existingNode.Previous };

        if (existingNode.Previous != null)
            existingNode.Previous.Next = newNode;
        else
            _head = newNode;

        existingNode.Previous = newNode;

        Count++;
    }

    public void AddAfter(DoublyNode<T> existingNode, T newData)
    {
        if (existingNode == null)
            throw new ArgumentNullException(nameof(existingNode), "Existing node cannot be null.");

        if (!NodeBelongsToList(existingNode))
            throw new InvalidOperationException("The provided node does not belong to this list.");

        DoublyNode<T> newNode = new(newData) { Next = existingNode.Next, Previous = existingNode };

        if (existingNode.Next != null)
            existingNode.Next.Previous = newNode;
        else
            _tail = newNode;

        existingNode.Next = newNode;

        Count++;
    }

    bool NodeBelongsToList(DoublyNode<T> node)
    {
        DoublyNode<T>? current = _head;

        while (current != null)
        {
            if (ReferenceEquals(current, node))
                return true;

            current = current.Next;
        }

        return false;
    }
    
    public bool Remove(T data)
    {
        DoublyNode<T>? current = _head;

        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, data))
                break;
            current = current.Next;
        }

        if (current is null) return false;

        if (current.Next != null)
            current.Next.Previous = current.Previous;
        else
            _tail = current.Previous;

        if (current.Previous != null)
            current.Previous.Next = current.Next;
        else
            _head = current.Next;

        Count--;
        return true;
    }

    public bool IsEmpty => Count == 0;

    public void Clear() => (_head, _tail, Count) = (null, null, 0);

    public bool Contains(T data)
    {
        DoublyNode<T>? current = _head;
        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, data))
                return true;
            current = current.Next;
        }

        return false;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<T> GetEnumerator()
    {
        DoublyNode<T>? current = _head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    public IEnumerable<T> BackEnumerator()
    {
        DoublyNode<T>? current = _tail;
        while (current != null)
        {
            yield return current.Data;
            current = current.Previous;
        }
    }

    public static DoublyLinkedList<T> operator +(DoublyLinkedList<T> list1, DoublyLinkedList<T> list2) => new(list1, list2);
    
}

//First and First - Stack
//Last and Last - Stack
//First and Last - Queue
//Last and First - Queue