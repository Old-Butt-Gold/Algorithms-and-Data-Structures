using System.Collections;
using AaDS.shared;

namespace AaDS.DataStructures.Heap;

class DHeap<T> : IEnumerable<T> where T : IComparable<T>
{
    List<T> _data = [];
    public bool IsMinHeap { get; }
    readonly CustomComparer<T> _comparer;
    readonly int _d;  // The "D" in the D-ary Heap

    public DHeap(int d, SortDirection sortDirection = SortDirection.Ascending)
    {
        if (d < 2)
            throw new ArgumentException("D must be greater than or equal to 2", nameof(d));

        _d = d;
        IsMinHeap = sortDirection == SortDirection.Ascending;
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
    }
    
    public DHeap(IEnumerable<T> collection, int d, SortDirection sortDirection = SortDirection.Ascending) : this(d, sortDirection)
    {
        _data = collection.ToList();
        for (int i = Count / _d; i > -1; i--)
            Heapify(i);
    }
    
    public int Count => _data.Count;

    public bool IsEmpty => Count == 0;

    // Returns the parent node index of node at index i 
    int Parent(int i) => (i - 1) / _d;

    // Get index of the k-th child of node at index i
    int Child(int i, int k) => _d * i + k + 1;
    
    public void Enqueue(T item)
    {
        _data.Add(item);
        int i = Count - 1;

        while (i > 0 && _comparer.Compare(_data[Parent(i)], _data[i]) > 0)
        {
            (_data[i], _data[Parent(i)]) = (_data[Parent(i)], _data[i]);
            i = Parent(i);
        }
    }
    
    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        var front = _data[0];
        _data[0] = _data[^1];
        _data.RemoveAt(Count - 1);
        Heapify(0);

        return front;
    }
    
    void Heapify(int index)
    {
        int item = index;

        for (int k = 0; k <= _d; k++)
        {
            int childIndex = Child(index, k);
            if (childIndex < Count && _comparer.Compare(_data[childIndex], _data[item]) < 0)
                item = childIndex;
        }

        if (item != index)
        {
            (_data[index], _data[item]) = (_data[item], _data[index]);
            Heapify(item);
        }
    }
    
    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        return _data[0];
    }

    public void Delete(int index)
    {
        DecreaseKey(index, _data[0]);
        Dequeue();
    }

    public void DecreaseKey(int index, T newValue) // IncreaseForMaxHeap
    {
        _data[index] = newValue;
        
        while (index > 0 && _comparer.Compare(_data[Parent(index)], _data[index]) > 0)
        {
            (_data[index], _data[Parent(index)]) = (_data[Parent(index)], _data[index]);
            index = Parent(index);
        }
    }

    public void Clear() => _data.Clear();
    
    public T[] ToArray() => _data.ToArray();
    
    public void EnqueueRange(IEnumerable<T> items)
    {
        foreach (var item in items)
            Enqueue(item);
    }
    
    public List<T> ExtractAll()
    {
        List<T> list = [];
        while (!IsEmpty)
            list.Add(Dequeue());
        return list;
    }
    
    public IEnumerator<T> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
