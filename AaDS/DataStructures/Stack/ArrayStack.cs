using System.Collections;

namespace AaDS.DataStructures.Stack;

class ArrayStack<T> : IEnumerable<T>
{
    T?[] _items;
    public int Count { get; private set; }
    const int InitialCapacity = 8;

    public ArrayStack() : this(InitialCapacity) { }

    public ArrayStack(int length)
    {
        if (length < 1)
            throw new ArgumentOutOfRangeException(nameof(length), "Длина должна быть положительной.");

        _items = new T[length];
    }

    public void Clear()
    {
        Array.Clear(_items, 0, Count);
        Count = 0;
    }
    
    public bool IsEmpty => Count == 0;

    public void Push(T? item)
    {
        if (Count == _items.Length)
            Resize(_items.Length * 2);
        _items[Count++] = item;
    }

    public T? Pop()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Стек пуст");

        T? item = _items[--Count];
        _items[Count] = default;

        if (Count > 0 && Count < _items.Length / 4)
            Resize(_items.Length / 2);

        return item;
    }

    public T? Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Стек пуст");

        return _items[Count - 1];
    }

    void Resize(int max)
    {
        T?[] tempItems = new T[max];
        Array.Copy(_items, tempItems, Count);
        _items = tempItems;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return _items[i]!;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
}