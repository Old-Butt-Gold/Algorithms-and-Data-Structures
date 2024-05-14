using System.Collections;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.LinkedList;

class DoublyCircularLinkedList<T> : IEnumerable<T>
{
    DoublyNode<T>? _head;
    public int Count { get; private set; }

    public void Add(T data)
    {
        DoublyNode<T>? node = new(data);

        if (_head == null)
        {
            _head = node;
            _head!.Next = _head.Previous = node;
        }
        else
        {
            node.Previous = _head.Previous;
            node.Next = _head;
            _head.Previous!.Next = node;
            _head.Previous = node;
        }

        Count++;
    }

    public bool Remove(T data)
    {
        if (Count == 0) return false;
        DoublyNode<T>? current = _head;
        DoublyNode<T>? removedItem = null;

        do
        {
            if (EqualityComparer<T>.Default.Equals(data, current!.Data))
            {
                removedItem = current;
                break;
            }

            current = current.Next;
        } while (current != _head);

        if (removedItem == null) return false;
        if (Count == 1)
            _head = null;
        else
        {
            if (removedItem == _head)
                _head = _head.Next;
            removedItem.Previous!.Next = removedItem.Next;
            removedItem.Next!.Previous = removedItem.Previous;
        }

        Count--;
        return true;
    }

    public bool IsEmpty => Count == 0;

    public void Clear() => (_head, Count) = (null, 0);

    public bool Contains(T data)
    {
        DoublyNode<T>? current = _head;
        do
        {
            if (EqualityComparer<T>.Default.Equals(data, current!.Data))
                return true;
            current = current.Next;
        } while (current != _head);

        return false;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<T> GetEnumerator()
    {
        DoublyNode<T>? current = _head;
        do
        {
            if (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        } while (current != _head);
    }

    public IEnumerable<T> InfinityEnumerator()
    {
        DoublyNode<T>? current = _head;
        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }
}