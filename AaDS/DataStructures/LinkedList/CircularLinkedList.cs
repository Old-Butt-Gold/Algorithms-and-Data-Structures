using System.Collections;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.LinkedList;

class CircularLinkedList<T> : IEnumerable<T>
{
    Node<T>? _head;
    Node<T>? _tail;
    public int Count { get; private set; }

    public void Add(T data)
    {
        Node<T>? node = new(data);
        if (_head == null)
        {
            _head = _tail = node;
            _tail.Next = _head;
        }
        else
        {
            node.Next = _head;
            _tail!.Next = node;
            _tail = node;
        }

        Count++;
    }

    public bool Remove(T data)
    {
        if (IsEmpty) return false;
        Node<T>? current = _head;
        Node<T>? previous = null;
        do
        {
            if (EqualityComparer<T>.Default.Equals(data, current!.Data))
            {
                if (previous != null)
                {
                    previous.Next = current!.Next;
                    if (current == _tail)
                        _tail = previous;
                }
                else
                {
                    if (Count == 1)
                        _head = _tail = null;
                    else
                    {
                        _head = current.Next;
                        _tail!.Next = current.Next;
                    }
                }

                Count--;
                return true;
            }

            previous = current;
            current = current.Next;
        } while (current != _head);

        return false;
    }

    public bool IsEmpty => Count == 0;

    public void Clear() => (_head, _tail, Count) = (null, null, 0);

    public bool Contains(T data)
    {
        Node<T>? current = _head;
        if (current == null) return false;
        do
        {
            if (EqualityComparer<T>.Default.Equals(data, current!.Data)) return true;
            current = current.Next;
        } while (current != _head);

        return false;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? current = _head;
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
        Node<T>? current = _head;
        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

}