using System.Collections;
using AaDS.shared;

namespace AaDS.DataStructures.Heap;

class BinaryHeap<T> : IEnumerable<T> where T : IComparable<T>//As PriorityQueue
{
    List<T> _data = new();
    public bool IsMinHeap { get; }
    readonly CustomComparer<T> _comparer;

    public BinaryHeap(SortDirection sortDirection = SortDirection.Ascending)
    {
        IsMinHeap = sortDirection == SortDirection.Ascending;
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
    }
    
    public BinaryHeap(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending) : this(sortDirection)
    {
        _data = collection.ToList();
        for (int i = Count / 2; i >= 0; i--)
            HeapifyDown(i);
    }
    
    public bool Remove(T item)
    {
        int index = _data.IndexOf(item);
        if (index == -1)
            return false;

        _data[index] = _data[^1];
        _data.RemoveAt(_data.Count - 1);

        int parent = (index - 1) / 2;
        if (index > 0 && _comparer.Compare(_data[index], _data[parent]) < 0)
            HeapifyUp(index);
        else
            HeapifyDown(index);

        return true;
    }

    public List<T> ExtractAll()
    {
        List<T> list = new();
        while (!IsEmpty)
            list.Add(Dequeue());
        return list;
    }
    
    public int Count => _data.Count;

    public bool IsEmpty => Count == 0;

    public void Enqueue(T item)
    {
        _data.Add(item);
        HeapifyUp(Count - 1);
    }

    void HeapifyUp(int index) //In MaxHeap change comparative sign in comparer(there it's changes by sortDirection)
    {
        int parent = (index - 1) / 2;

        if (parent > -1 && _comparer.Compare(_data[index], _data[parent]) < 0)
        {
            (_data[index], _data[parent]) = (_data[parent], _data[index]);
            HeapifyUp(parent);
        }
    }
    
    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        return _data[0];
    }

    public void Clear() => _data.Clear();
    
    public bool Contains(T item) => _data.Contains(item);
    
    public T[] ToArray() => _data.ToArray();
    
    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        T front = _data[0];
        _data[0] = _data[^1];
        _data.RemoveAt(Count - 1);
        HeapifyDown(0);

        return front;
    }

    public void EnqueueRange(IEnumerable<T> items)
    {
        foreach (var item in items)
            Enqueue(item);
    }
    
    void HeapifyDown(int index) //In MaxHeap change comparative sign in comparer(there it's changes by sortDirection)
    {
        int left = 2 * index + 1;
        int right = 2 * index + 2;
        int smallest = index;

        if (left < Count && _comparer.Compare(_data[left], _data[smallest]) < 0) 
            smallest = left;

        if (right < Count && _comparer.Compare(_data[right], _data[smallest]) < 0)
            smallest = right;

        if (smallest != index)
        {
            (_data[index], _data[smallest]) = (_data[smallest], _data[index]);
            HeapifyDown(smallest);
        }
    }
    
    public IEnumerator<T> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}