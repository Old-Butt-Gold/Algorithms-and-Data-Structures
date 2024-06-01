using System.Collections;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Queue;

class Queue<T> : IEnumerable<T>
{
    Node<T>? _head; 
    Node<T>? _tail; 

    public int Count { get; private set; }
        
    public void Enqueue(T data)
    {
        Node<T> node = new(data);
 
        if (_head == null)
            _head = node;
        else
            _tail!.Next = node;
        _tail = node;
        Count++;
    }

    public T Dequeue()
    {
        if (Count == 0) throw new InvalidOperationException();
        T output = _head!.Data;
        _head = _head.Next;
        Count--;
        return output;
    }

    public bool TryDequeue(out T? result)
    {
        if (!IsEmpty)
        {
            result = _head!.Data;
            _head = _head.Next;
            Count--;
            return true;
        }
        result = default;
        return false;
    }

    public bool TryPeek(out T? result)
    {
        result = _head is null ? default : _head.Data;
        return !IsEmpty;
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

    public bool IsEmpty => Count == 0;

    public void Clear() => (_head, _tail, Count) = (null, null, 0);
 
    public bool Contains(T data)
    {
        Node<T>? current = _head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, data)) return true;
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
        
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); //Явная реализация интерфейса
        
}