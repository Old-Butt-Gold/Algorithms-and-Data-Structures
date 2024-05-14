//Работает Max, Sum, Mul, Min

using System.Collections;

namespace AaDS.DataStructures.Tree;

public class SegmentTree : IEnumerable<int>
{
    int[] _tree;
    int[] _array;
    readonly Func<int, int, int>? _operation;
    public int Count { get; private set; }

    public SegmentTree(IEnumerable<int> inputArray, Func<int, int, int>? operation = null)
    {
        _array = inputArray.ToArray();
        _operation = operation ?? ((a, b) => a + b);
        Count = _array.Length;

        int height = (int)Math.Ceiling(Math.Log2(Count));
        int treeSize = 2 * (int)Math.Pow(2, height) - 1;
        _tree = new int[treeSize];

        BuildTree(0, 0, Count - 1);
    }

    int BuildTree(int index, int start, int end)
    {
        if (start == end)
        {
            _tree[index] = _array[start];
            return _tree[index];
        }

        int mid = (start + end) / 2;
        _tree[index] = _operation(BuildTree(2 * index + 1, start, mid), BuildTree(2 * index + 2, mid + 1, end));
        return _tree[index];
    }

    public int RangeQuery(int queryStart, int queryEnd)
    {
        if (queryStart > queryEnd)
            (queryStart, queryEnd) = (queryEnd, queryStart);

        /*
        queryStart = Math.Max(queryStart, 0);
        queryEnd = Math.Min(queryEnd, Count - 1);
    */

        if (queryStart < 0 || queryEnd >= Count)
        {
            return DefaultQueryValue();
        }

        return RangeQuery(0, 0, Count - 1, queryStart, queryEnd);
    }

    int RangeQuery(int index, int start, int end, int queryStart, int queryEnd)
    {
        // Обработка некорректных индексов
        if (queryStart > end || queryEnd < start)
            return DefaultQueryValue();

        // Запрос полностью включен в текущий отрезок
        if (queryStart <= start && queryEnd >= end)
            return _tree[index];

        // Запрос частично перекрывается с текущим отрезком
        int mid = (start + end) / 2;
        return _operation(
            RangeQuery(2 * index + 1, start, mid, queryStart, queryEnd),
            RangeQuery(2 * index + 2, mid + 1, end, queryStart, queryEnd)
        );
    }

    int DefaultQueryValue() => _operation(10, -10) switch
    {
        10 => int.MinValue, //Max
        -10 => int.MaxValue, //Min
        -100 => 1, //Multiply
        _ => 0 //Sum(10 - 10 == 0)
    };

    public void Update(int index, int newValue)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentException("Invalid index.");

        Update(0, 0, Count - 1, index, newValue);
    }

    void Update(int treeIndex, int start, int end, int index, int newValue)
    {
        if (start == end)
        {
            _array[index] = newValue;
            _tree[treeIndex] = newValue;
            return;
        }

        int mid = (start + end) / 2;
        if (index <= mid)
        {
            Update(2 * treeIndex + 1, start, mid, index, newValue);
        }
        else
        {
            Update(2 * treeIndex + 2, mid + 1, end, index, newValue);
        }

        _tree[treeIndex] = _operation(_tree[2 * treeIndex + 1], _tree[2 * treeIndex + 2]);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<int> GetEnumerator() => _array.AsEnumerable().GetEnumerator();
}