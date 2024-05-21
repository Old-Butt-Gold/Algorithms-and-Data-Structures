using System.Collections;

namespace AaDS.DataStructures.Set;

/// <summary>
/// Represents an unordered sparse set of natural numbers, and provides constant-time operations on it.
/// </summary>
public class SparseSet : IEnumerable<int>
{
    readonly int[] _dense;
    readonly int[] _sparse;
    readonly int _max;
    public int Count { get; private set; }

    public SparseSet(int maxVal)
    {
        _max = maxVal + 1;
        _sparse = new int[_max];
        _dense = new int[_max];
    }

    public bool Add(int value)
    {
        if (value > -1 && value < _max && !Contains(value))
        {
            _dense[Count] = value;
            _sparse[value] = Count;
            Count++;
            return true;
        }

        return false;
    }

    public bool Contains(int value)
    {
        if (value >= _max || value < 0)
        {
            return false;
        }

        return _sparse[value] < Count && _dense[_sparse[value]] == value;
    }

    public void Remove(int value)
    {
        if (Contains(value))
        {
            _dense[_sparse[value]] = _dense[Count - 1];

            _sparse[_dense[Count - 1]] = _sparse[value];

            Count--;
        }
    }

    public void Clear() => Count = 0;
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<int> GetEnumerator() => _dense.Take(Count).GetEnumerator();
}