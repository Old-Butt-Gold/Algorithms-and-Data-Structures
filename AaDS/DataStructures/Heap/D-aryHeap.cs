using System.Collections;
using AaDS.shared;

namespace AaDS.DataStructures.Heap;

class DaryHeap<T> : IEnumerable<T> where T : IComparable
{
    readonly IComparer<T> _comparer;
    public bool IsMinHeap { get; }
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;

    List<T> heapArray = new List<T>();
    readonly int children;

    public DaryHeap(int children, SortDirection sortDirection = SortDirection.Ascending)
    {
        IsMinHeap = sortDirection == SortDirection.Ascending;
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        if (children <= 2) throw new Exception("Number of nodes d must be greater than 2.");

        this.children = children;
    }

    public DaryHeap(int children, IEnumerable<T> initial, SortDirection sortDirection = SortDirection.Ascending) 
        : this(children, sortDirection)
    {
        if (initial is null)
            throw new ArgumentNullException();
        heapArray.AddRange(initial);
        Count = heapArray.Count;
        BuildHeap();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<T> GetEnumerator() => heapArray.GetEnumerator();

    void BuildHeap()
    {
        var lastNonLeafIndex = (heapArray.Count - 1) / children;
        for (int i = lastNonLeafIndex; i > -1; i--)
        {
            HeapifyDown(i);
        }
    }
    
    public void Insert(T newItem)
    {
        heapArray.Add(newItem);
        Count++;
        HeapifyUp(Count - 1);
    }

    void HeapifyUp(int index)
    {
        if (index < 1)
            return;

        int parentIndex = (index - 1) / children;

        if (_comparer.Compare(heapArray[index], heapArray[parentIndex]) < 0)
        {
            (heapArray[index], heapArray[parentIndex]) = (heapArray[parentIndex], heapArray[index]);
            HeapifyUp(parentIndex);
        }
    }

    public T Extract()
    {
        if (Count == 0)
            throw new Exception("Empty heap");

        T minMax = heapArray[0];

        heapArray[0] = heapArray[Count - 1];
        heapArray.RemoveAt(Count - 1);
        Count--;

        HeapifyDown(0);
    
        return minMax;
    }

    void HeapifyDown(int index)
    {
        int minMaxChildIndex = FindMinMaxChildIndex(index);

        if (minMaxChildIndex != -1 && _comparer.Compare(heapArray[index], heapArray[minMaxChildIndex]) > 0)
        {
            (heapArray[index], heapArray[minMaxChildIndex]) = (heapArray[minMaxChildIndex], heapArray[index]);
            HeapifyDown(minMaxChildIndex);
        }
    }

    int FindMinMaxChildIndex(int currentParent)
    {
        
        var currentMinMax = currentParent * children + 1;

        if (currentMinMax >= Count)
            return -1;

        for (var i = 2; i <= children; i++)
        {
            var newIndex = currentParent * children + i;
            if (newIndex >= Count)
                break;

            var nextSibling = heapArray[newIndex];

            if (_comparer.Compare(heapArray[currentMinMax], nextSibling) > 0) 
                currentMinMax = newIndex;
        }

        return currentMinMax;
    }

    public T Peek()
    {
        if (Count == 0) 
            throw new Exception("Empty heap");

        return heapArray[0];
    }

}