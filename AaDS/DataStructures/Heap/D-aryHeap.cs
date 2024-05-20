using System.Collections;
using AaDS.shared;

namespace AaDS.DataStructures.Heap;

class DaryHeap<T> : IEnumerable<T> where T : IComparable
{
    readonly IComparer<T> _comparer;
    public bool IsMinHeap { get; }
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;

    List<T> _heapArray = new List<T>();
    readonly int _children;

    public DaryHeap(int children, SortDirection sortDirection = SortDirection.Ascending)
    {
        IsMinHeap = sortDirection == SortDirection.Ascending;
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        if (children <= 2) throw new Exception("Number of nodes d must be greater than 2.");

        this._children = children;
    }

    public DaryHeap(int children, IEnumerable<T> initial, SortDirection sortDirection = SortDirection.Ascending) 
        : this(children, sortDirection)
    {
        if (initial is null)
            throw new ArgumentNullException();
        _heapArray.AddRange(initial);
        Count = _heapArray.Count;
        BuildHeap();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<T> GetEnumerator() => _heapArray.GetEnumerator();

    void BuildHeap()
    {
        var lastNonLeafIndex = (_heapArray.Count - 1) / _children;
        for (int i = lastNonLeafIndex; i > -1; i--)
        {
            HeapifyDown(i);
        }
    }
    
    public void Insert(T newItem)
    {
        _heapArray.Add(newItem);
        Count++;
        HeapifyUp(Count - 1);
    }

    void HeapifyUp(int index)
    {
        if (index < 1)
            return;

        int parentIndex = (index - 1) / _children;

        if (_comparer.Compare(_heapArray[index], _heapArray[parentIndex]) < 0)
        {
            (_heapArray[index], _heapArray[parentIndex]) = (_heapArray[parentIndex], _heapArray[index]);
            HeapifyUp(parentIndex);
        }
    }

    public T Extract()
    {
        if (Count == 0)
            throw new Exception("Empty heap");

        T minMax = _heapArray[0];

        _heapArray[0] = _heapArray[Count - 1];
        _heapArray.RemoveAt(Count - 1);
        Count--;

        HeapifyDown(0);
    
        return minMax;
    }

    void HeapifyDown(int index)
    {
        int minMaxChildIndex = FindMinMaxChildIndex(index);

        if (minMaxChildIndex != -1 && _comparer.Compare(_heapArray[index], _heapArray[minMaxChildIndex]) > 0)
        {
            (_heapArray[index], _heapArray[minMaxChildIndex]) = (_heapArray[minMaxChildIndex], _heapArray[index]);
            HeapifyDown(minMaxChildIndex);
        }
    }

    int FindMinMaxChildIndex(int currentParent)
    {
        
        var currentMinMax = currentParent * _children + 1;

        if (currentMinMax >= Count)
            return -1;

        for (var i = 2; i <= _children; i++)
        {
            var newIndex = currentParent * _children + i;
            if (newIndex >= Count)
                break;

            var nextSibling = _heapArray[newIndex];

            if (_comparer.Compare(_heapArray[currentMinMax], nextSibling) > 0) 
                currentMinMax = newIndex;
        }

        return currentMinMax;
    }

    public T Peek()
    {
        if (Count == 0) 
            throw new Exception("Empty heap");

        return _heapArray[0];
    }

}