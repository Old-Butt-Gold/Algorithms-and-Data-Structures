using System.Collections;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Stack;

class Stack<T> : IEnumerable<T>
{
    Node<T>? _head;
    public int Count { get; private set; }
 
    public bool IsEmpty => Count == 0;
    
    public void Push(T item)
    {
        _head = new(item) { Next = _head };
        Count++;
    }
    
    public void Reverse()
    {
        if (_head is null) return;
        Node<T>? temp = _head.Next;
        _head!.Next = null;
        while (temp != null)
        {
            Node<T> next = new (temp.Data) { Next = _head };
            temp = temp.Next;
            _head = next;
        }
    }
    
    public T Pop()
    {
        if (IsEmpty) throw new InvalidOperationException("Стек пуст");
        Node<T>? temp = _head;
        _head = _head?.Next;
        Count--;
        return temp!.Data;
    }
    
    public bool TryPop(out T? result)
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
    
    public T Peek()
    {
        if (IsEmpty) throw new InvalidOperationException("Стек пуст");
        return _head!.Data;
    }

    public bool TryPeek(out T? result)
    {
        result = _head is null ? default : _head.Data;
        return !IsEmpty;
    }

    public void Clear() => (_head, Count) = (null, 0);

    public T[] ToArray()
    {
        T[] array = new T[Count];
        int index = 0;
        Node<T>? current = _head;

        while (current != null)
        {
            array[index++] = current.Data;
            current = current.Next;
        }
        return array;
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

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}