using System.Collections;

namespace AaDS.DataStructures.HashSet;

public class SeparateChainingSet<T> : ISet<T>
{
    class Node<T>
    {
        public T Data;
        public Node<T>? Next;
        public Node<T>? After, Previous; 
        //Используются для вывода элементов в порядке их забрасывания во множество
        public Node(T data) => Data = data;
    }

    const int DefaultSize = 32;
    Node<T>?[] _items;
    Node<T>? _head, _tail;
    public int Count { get; private set; }

    public bool IsEmpty => Count == 0;
    
    public bool IsReadOnly => false;

    public SeparateChainingSet() : this(DefaultSize) { }

    public SeparateChainingSet(int capacity) => _items = new Node<T>[capacity];
    
    public SeparateChainingSet(params IEnumerable<T>[] collections) : this(DefaultSize)
    {
        foreach (IEnumerable<T> collection in collections)
            foreach (T item in collection)
                Add(item);
    }

    int GetHash(T value) => (value!.GetHashCode() & 0x7FFFFFFF) % _items!.Length;

    void AddNode(Node<T>? newNode)
    {
        if (_head == null)
            _head = newNode;
        else
        {
            _tail!.After = newNode!;
            newNode!.Previous = _tail;
        }
        _tail = newNode;
    }

    void RemoveNode(Node<T>? newNode)
    {
        if (newNode?.After != null)
            newNode.After.Previous = newNode.Previous;
        else
            _tail = newNode?.Previous;

        if (newNode?.Previous != null)
            newNode.Previous.After = newNode.After;
        else
            _head = newNode?.After;
    }

    public bool Add(T value) {
        int index = GetHash(value);
        Node<T>? current = _items[index];
        while (current != null) {
            if (EqualityComparer<T>.Default.Equals(current.Data, value)) {
                return false;
            }
            current = current.Next;
        }
        
        Node<T> newNode = new(value);
        newNode.Next = _items[index];
        _items[index] = newNode;
        AddNode(newNode);
        Count++;
        Grow();
        
        return true;
    }
 
    void Grow() {
        if (Count > 0.75 * _items.Length)
        {
            Node<T>?[] newItems = new Node<T>[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
            {
                Node<T>? current = _items[i];
                while (current != null)
                {
                    Node<T>? next = current.Next;
                    int newIndex = current.Data!.GetHashCode() & 0x7FFFFFFF % newItems.Length;
                    current.Next = newItems[newIndex];
                    newItems[newIndex] = current;
                    current = next;
                }
            }

            _items = newItems;
        }
    }

    void Shrink()
    {
        if (Count <= 0.3 * _items.Length && _items.Length > DefaultSize)
        {
            Node<T>?[] newItems = new Node<T>[_items.Length / 2];
            for (int i = 0; i < _items.Length; i++)
            {
                Node<T>? current = _items[i];
                while (current != null)
                {
                    Node<T>? next = current.Next;
                    int newIndex = current.Data!.GetHashCode() & 0x7FFFFFFF % newItems.Length;
                    current.Next = newItems[newIndex];
                    newItems[newIndex] = current;
                    current = next;
                }
            }

            _items = newItems;
        }
    }

    public void Clear()
    {
        for (int i = 0; i < _items.Length; i++)
            _items[i] = null;
        (_head, _tail, Count) = (null, null, 0);
    }
    
    public int RemoveWhere(Predicate<T> match)
    {
        if (match == null)
            throw new ArgumentNullException(nameof(match));
        int count = 0;

        foreach (var item in this)
        {
            if (match(item))
            {
                Remove(item);
                count++;
            }
        }

        return count;
    }

    public bool Contains(T item)
    {
        Node<T>? current = _items[GetHash(item)];
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, item))
                return true;
            current = current.Next;
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));

        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        if (array.Length - arrayIndex < Count)   
            throw new ArgumentException("The destination array is not large enough.");

        Node<T>? current = _head;
        while (current != null)
        {
            array[arrayIndex++] = current.Data;
            current = current.After;
        }
    }

    public void ExceptWith(IEnumerable<T> other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        foreach (var item in other)
            Remove(item);
    }

    public void IntersectWith(IEnumerable<T> other)
    {
        if (other is null) 
            throw new ArgumentNullException(nameof(other));
 
        SeparateChainingSet<T> tempSet = new(other);
        foreach (var item in this)
            if (!tempSet.Contains(item))
                Remove(item);
    }

    public bool Remove(T item)
    {
        int index = GetHash(item);
        Node<T>? current = _items[index];
        Node<T>? previous = null;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, item))
            {
                RemoveNode(current);
                if (previous == null)
                    _items[index] = current.Next;
                else
                    previous.Next = current.Next;

                Count--;
                Shrink();
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }

    public bool SetEquals(IEnumerable<T> other)
    {
        if (other == null) 
            throw new ArgumentNullException(nameof(other));
 
        SeparateChainingSet<T> otherSet = new(other);
 
        if (Count != otherSet.Count)
            return false;
 
        return ContainsAllElements(otherSet);
    }
    
    public bool ContainsAllElements(IEnumerable<T> other)
    {
        foreach (T item in other)
            if (!Contains(item))
                return false;
 
        return true;
    }
    
    public void SymmetricExceptWith(IEnumerable<T> other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        foreach (var item in other)
            if (Contains(item))
                Remove(item);
            else
                Add(item);
    }

    public void UnionWith(IEnumerable<T> other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        foreach (var item in other)
            Add(item);
    }
    
    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
        if (other is null) 
            throw new ArgumentNullException(nameof(other));
 
        SeparateChainingSet<T> otherSet = new(other);
 
        if (Count >= otherSet.Count)
            return false;
 
        return IsSubsetOf(otherSet);
    }
 
    public bool IsSubsetOf(IEnumerable<T> other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));
 
        SeparateChainingSet<T> otherSet = new(other);
 
        if (Count > otherSet.Count)
            return false;
 
        return IsSubsetOf(otherSet);
    }
 
    bool IsSubsetOf(SeparateChainingSet<T> other)
    {
        foreach (T item in this)
            if (!other.Contains(item))
                return false;
 
        return true;
    }
    
    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
        if (other is null) 
            throw new ArgumentNullException(nameof(other));
 
        SeparateChainingSet<T> otherSet = new(other);
 
        if (otherSet.Count >= Count)
            return false;
 
        return ContainsAllElements(otherSet);
 
    }
 
    public bool IsSupersetOf(IEnumerable<T> other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));
 
        SeparateChainingSet<T> otherSet = new(other);
 
        if (otherSet.Count > Count)
            return false;
 
        return ContainsAllElements(otherSet);
    }

    public bool Overlaps(IEnumerable<T> other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));
 
        SeparateChainingSet<T> otherSet = new(other);
 
        foreach (var item in otherSet)
            if (Contains(item))
                return true;
        return false;
    }

    void ICollection<T>.Add(T item) => Add(item);

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? current = _head;
        while (current != null)
        {
            yield return current.Data;
            current = current.After;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
}