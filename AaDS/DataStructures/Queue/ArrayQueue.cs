using System.Collections;

namespace AaDS.DataStructures.Queue;

//CircularQueue
class ArrayQueue<T> : IEnumerable<T>
{
    T[] _items;
    public int Count { get; private set; }
    int _front;
    int _rear;
    const int InitialCapacity = 8;

    public ArrayQueue() : this(InitialCapacity) { }

    public ArrayQueue(int size)
    {
        if (size < 1)
            throw new ArgumentOutOfRangeException(nameof(size), "Размер должен быть положительным.");

        _items = new T[size];
        _rear = -1;
    }

    public bool IsEmpty => Count == 0;

    public int Capacity => _items.Length;

    public void Enqueue(T item)
    {
        if (Count == _items.Length)
            Resize(_items.Length * 2);
        _rear = (_rear + 1) % _items.Length;
        _items[_rear] = item;
        Count++;
    }

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Очередь пуста");
        T item = _items[_front];
        _items[_front] = default;
        _front = (_front + 1) % _items.Length;
        Count--;
        
        if (Count > InitialCapacity && Count == _items.Length / 4)
            Resize(_items.Length / 2);
        
        return item;
    }

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Очередь пуста");
        return _items[_front];
    }

    public void Clear()
    {
        Array.Clear(_items, 0, _items.Length);
        _front = 0;
        _rear = -1;
        Count = 0;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
            yield return _items[(_front + i) % _items.Length];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    void Resize(int max)
    {
        T[] tempItems = new T[max];
        for (int i = 0; i < Count; i++)
            tempItems[i] = _items[(_front + i) % _items.Length];
        _items = tempItems;
        _front = 0;
        _rear = Count - 1;
    }
}