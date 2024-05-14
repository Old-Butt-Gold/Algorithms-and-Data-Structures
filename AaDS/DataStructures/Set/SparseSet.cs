using System.Collections;

namespace AaDS.DataStructures.Set;

public class SparseSet : IEnumerable<int>
{
    readonly int[] _dense;
    readonly int[] _sparse;

    public SparseSet(int maxVal, int capacity)
    {
        _sparse = new int[maxVal + 1];
        Array.Fill(_sparse, -1);
        _dense = new int[capacity];
        Array.Fill(_dense, -1);
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<int> GetEnumerator() => _dense.Take(Count).GetEnumerator();

    public void Add(int value)
    {
        if (value < 0) 
            throw new Exception("Negative values not supported.");

        if (value >= _sparse.Length) 
            throw new Exception("Item is greater than max value.");

        if (Count >= _dense.Length) 
            throw new Exception("Set reached its capacity.");

        _sparse[value] = Count;
        _dense[Count] = value;
        Count++;
    }

    public void Remove(int value)
    {
        if (value < 0) 
            throw new Exception("Negative values not supported.");

        if (value >= _sparse.Length) 
            throw new Exception("Item is greater than max value.");

        if (!HasItem(value)) 
            throw new Exception("Item do not exist.");

        var index = _sparse[value];
        _sparse[value] = -1;

        var lastVal = _dense[Count - 1];
        _dense[index] = lastVal;
        _dense[Count - 1] = -1;

        _sparse[lastVal] = index;

        Count--;
    }

    public bool HasItem(int value)
    {
        var index = _sparse[value];
        return index != -1 && _dense[index] == value;
    }

    public void Clear() => Count = 0;
}