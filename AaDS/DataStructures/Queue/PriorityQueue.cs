using System.Collections;
using AaDS.DataStructures.Heap;
using AaDS.shared;

namespace AaDS.DataStructures.Queue;

class PriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
{
    BinaryHeap<T> _binaryHeap;
    
    public PriorityQueue(SortDirection sortDirection = SortDirection.Ascending) 
        => _binaryHeap = new(sortDirection);

    public PriorityQueue(IEnumerable<T> colleciton, SortDirection sortDirection = SortDirection.Ascending) 
        => _binaryHeap = new(colleciton, sortDirection);
    
    public int Count => _binaryHeap.Count;

    public bool IsEmpty => _binaryHeap.Count == 0;

    public void Enqueue(T item) => _binaryHeap.Enqueue(item);

    public T Peek() => _binaryHeap.Peek();

    public void Clear() => _binaryHeap.Clear();
    
    public T[] ToArray() => _binaryHeap.ToArray();

    public T Dequeue() => _binaryHeap.Dequeue();

    public void EnqueueRange(IEnumerable<T> items) => _binaryHeap.EnqueueRange(items);
    
    public IEnumerator<T> GetEnumerator() => _binaryHeap.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}