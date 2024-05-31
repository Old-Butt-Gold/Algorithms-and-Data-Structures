using System.Collections;
using AaDS.shared;

namespace AaDS.DataStructures.Heap;

class BinaryHeap<T> : IEnumerable<T> where T : IComparable<T>
{
    readonly List<T> _data = [];
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
            Heapify(i);
    }
    
    public int Count => _data.Count;

    public bool IsEmpty => Count == 0;

    //Returns the parent node index of node at index i 
    int Parent(int i) => (i - 1) / 2;

    //get index of left child of node at index i
    int Left(int i) => 2 * i + 1;

    //get index of right child of node at index i
    int Right(int i) => 2 * i + 2;
    
    public void Enqueue(T item)
    {
        _data.Add(item);

        int i = Count - 1;

        while (i > 0 && _comparer.Compare(_data[i], _data[Parent(i)]) < 0)
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
        int left = Left(index);
        int right = Right(index);
        int item = index;

        if (left < Count && _comparer.Compare(_data[left], _data[item]) < 0) 
            item = left;

        if (right < Count && _comparer.Compare(_data[right], _data[item]) < 0)
            item = right;

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

    public void DecreaseKey(int index, T newValue) //IncreaseForMaxHeap
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