using System.Collections;

namespace AaDS.DataStructures.Dictionary;

public class SeparateChainingDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    class Node<TKey, TValue>
    {
        public TKey Key;
        public TValue Data;
        public Node<TKey, TValue>? Next;
        public Node<TKey, TValue>? After, Previous; //Used for iterating on items in order of adding them
        public Node(TKey key, TValue data) => (Key, Data) = (key, data);
    }

    const int DefaultSize = 32;
    Node<TKey, TValue>?[] _items;

    Node<TKey, TValue>? _head, _tail;

    public int Count { get; private set; }

    public bool IsEmpty => Count == 0;
    
    public bool IsReadOnly => false;

    public SeparateChainingDictionary() : this(DefaultSize) { }

    public SeparateChainingDictionary(int capacity) => _items = new Node<TKey, TValue>[capacity];
    
    public SeparateChainingDictionary(params IEnumerable<KeyValuePair<TKey, TValue>>[] collections) : this(DefaultSize)
    {
        foreach (var collection in collections)
            foreach (var item in collection)
                Add(item);
    }

    int GetHash(TKey key) => (key!.GetHashCode() & 0x7FFFFFFF) % _items!.Length;
    
    void AddNode(Node<TKey, TValue>? newNode)
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

    void RemoveNode(Node<TKey, TValue>? newNode)
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

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = GetHash(key);
        Node<TKey, TValue>? current = _items[index];
        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
            {
                value = current.Data;
                return true;
            }
            current = current.Next;
        }
        value = default;
        return false;
    }

    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out var value))
                return value;
            throw new KeyNotFoundException();
        }
        set => Add(key, value, false);
    }

    public ICollection<TKey> Keys
    {
        get
        {
            List<TKey> keys = new List<TKey>(Count);
            Node<TKey, TValue>? current = _head;
            while (current != null)
            {
                keys.Add(current.Key);
                current = current.After;
            }
            return keys;
        }
    }

    public ICollection<TValue> Values
    {
        get
        {
            List<TValue> values = new List<TValue>(Count);
            Node<TKey, TValue>? current = _head;
            while (current != null)
            {
                values.Add(current.Data);
                current = current.After;
            }
            return values;
        }
    }
    
    public void Add(TKey key, TValue value) => Add(key, value, true);
    
    public void Add(KeyValuePair<TKey, TValue> pair) => Add(pair.Key, pair.Value, true);

    public void Add(TKey key, TValue value, bool isAdd)
    {
        int index = GetHash(key);
        Node<TKey, TValue>? current = _items[index];
        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
            {
                if (isAdd)
                    throw new ArgumentException();
                current.Data = value;
                return;
            }

            current = current.Next;
        }

        Node<TKey, TValue> newNode = new(key, value);
        newNode.Next = _items[index];
        _items[index] = newNode;
        AddNode(newNode);
        Count++;
        Grow();
    }

    void Grow()
    {
        if (Count >= 0.75 * _items.Length)
        {
            Node<TKey, TValue>?[] newItems = new Node<TKey, TValue>[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
            {
                Node<TKey, TValue>? current = _items[i];
                while (current != null)
                {
                    Node<TKey, TValue>? next = current.Next;
                    int newIndex = current.Key!.GetHashCode() & 0x7FFFFFFF % newItems.Length;
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
            Node<TKey, TValue>?[] newItems = new Node<TKey, TValue>[_items.Length / 2];
            for (int i = 0; i < _items.Length; i++)
            {
                Node<TKey, TValue>? current = _items[i];
                while (current != null)
                {
                    var next = current.Next;
                    int newIndex = current.Key!.GetHashCode() & 0x7FFFFFFF % newItems.Length;
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

    public bool ContainsValue(TValue value)
    {
        Node<TKey, TValue>? current = _head;
        while (current != null)
        {
            if (EqualityComparer<TValue>.Default.Equals(current.Data, value))
                return true;
            current = current.After;
        }
        return false;
    }
    
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        int index = GetHash(item.Key);
        var current = _items[index];
        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, item.Key) && EqualityComparer<TValue>.Default.Equals(current.Data, item.Value))
                return true;
            current = current.Next;
        }
        return false;
    }


    public bool ContainsKey(TKey key)
    {
        var current = _items[GetHash(key)];
        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                return true;
            current = current.Next;
        }
        return false;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));

        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        if (array.Length - arrayIndex < Count)   
            throw new ArgumentException("The destination array is not large enough.");

        Node<TKey, TValue>? current = _head;
        while (current != null)
        {
            array[arrayIndex++] = new(current.Key, current.Data);
            current = current.After;
        }
    }
    
    public bool Remove(TKey key, out TValue value)
    {
        int index = GetHash(key);
        Node<TKey, TValue>? current = _items[index];
        Node<TKey, TValue>? previous = null;
        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
            {
                RemoveNode(current);
                if (previous == null)
                    _items[index] = current.Next;
                else
                    previous.Next = current.Next;

                value = current.Data;
                Count--;
                Shrink();
                return true;
            }
            previous = current;
            current = current.Next;
        }
        value = default;
        return false;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        int index = GetHash(item.Key);
        Node<TKey, TValue>? current = _items[index];
        Node<TKey, TValue>? previous = null;
        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, item.Key) && EqualityComparer<TValue>.Default.Equals(current.Data, item.Value))
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
    
    public bool Remove(TKey key)
    {
        int index = GetHash(key);
        Node<TKey, TValue>? current = _items[index];
        Node<TKey, TValue>? previous = null;
        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
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

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        Node<TKey, TValue>? current = _head;
        while (current != null)
        {
            yield return new(current.Key, current.Data);
            current = current.After;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}