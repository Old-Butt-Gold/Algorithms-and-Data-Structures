using System.Collections;
using AaDS.shared;

namespace AaDS.DataStructures.Queue;

class PriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
{
    List<T> data = new();
    public bool IsMinHeap { get; }
    readonly CustomComparer<T> _comparer;
    
    public PriorityQueue(SortDirection sortDirection = SortDirection.Ascending)
    {
        IsMinHeap = sortDirection == SortDirection.Ascending;
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
    }
    
    public int Count => data.Count;

    public bool IsEmpty => Count == 0;

    public void Enqueue(T item)
    {
        data.Add(item);
        HeapifyUp(Count - 1);
    }

    void HeapifyUp(int index) //в MaxHeap менять знак сравнения
    {
        int parent = (index - 1) / 2;

        if (parent > -1 && _comparer.Compare(data[index], data[parent]) < 0)
        {
            (data[index], data[parent]) = (data[parent], data[index]);
            HeapifyUp(parent);
        }
    }
    
    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        return data[0];
    }

    public void Clear() => data.Clear();
    
    public bool Contains(T item) => data.Contains(item);
    
    public T[] ToArray() => data.ToArray();
    
    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        T front = data[0];
        data[0] = data[^1];
        data.RemoveAt(Count - 1);
        HeapifyDown(0);

        return front;
    }

    public void EnqueueRange(IEnumerable<T> items)
    {
        foreach (var item in items)
            Enqueue(item);
    }
    
    void HeapifyDown(int index) //в MaxHeap менять знак сравнения
    {
        int left = 2 * index + 1;
        int right = 2 * index + 2;
        int smallest = index;

        if (left < Count && _comparer.Compare(data[left], data[smallest]) < 0) 
            smallest = left;

        if (right < Count && _comparer.Compare(data[right], data[smallest]) < 0)
            smallest = right;

        if (smallest != index)
        {
            (data[index], data[smallest]) = (data[smallest], data[index]);
            HeapifyDown(smallest);
        }
    }
    
    public IEnumerator<T> GetEnumerator() => data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}