using System.Collections;

namespace AaDS.DataStructures.Tree;

class SegmentTree<T> : IEnumerable<T>
{
    private T[] _tree;
    private T[] _data;
    public int Count { get; private set; }
    
    private readonly Func<T, T, T> _operation;
    private readonly T _identityElement;

    //1. сложение SegmentTree<int> sumTree = new SegmentTree<int>(input, (a, b) => a + b, 0);
    //2. умножение SegmentTree<int> productTree = new SegmentTree<int>(input, (a, b) => a * b, 1);
    //3. минимум SegmentTree<int> minTree = new SegmentTree<int>(input, (a, b) => Math.Min(a, b), int.MaxValue);
    //4. максимум SegmentTree<int> maxTree = new SegmentTree<int>(input, (a, b) => Math.Max(a, b), int.MinValue);
    //5. Побитовое И SegmentTree<int> andTree = new SegmentTree<int>(input, (a, b) => a & b, ~0);
    //6. Побитовое ИЛИ SegmentTree<int> orTree = new SegmentTree<int>(input, (a, b) => a | b, 0);
    //7. Побитовое XOR SegmentTree<int> orTree = new SegmentTree<int>(input, (a, b) => a ^ b, 0);
    //8. конкатенация строк SegmentTree<string> concatTree = new SegmentTree<string>(stringInput, (a, b) => a + b, "");
    public SegmentTree(IEnumerable<T> input, Func<T, T, T> operation, T identityElement)
    {
        Count = input.Count();
        _data = new T[Count];
        _data = input.ToArray();
        _tree = new T[4 * Count];
        _operation = operation;
        _identityElement = identityElement;
        Build(0, 0, Count - 1);
    }

    void Build(int node, int start, int end)
    {
        if (start == end)
        {
            _tree[node] = _data[start];
        }
        else
        {
            int mid = (start + end) / 2;
            int leftChild = 2 * node + 1;
            int rightChild = 2 * node + 2;

            Build(leftChild, start, mid);
            Build(rightChild, mid + 1, end);

            _tree[node] = _operation(_tree[leftChild], _tree[rightChild]);
        }
    }

    public void Update(int idx, T value) 
        => Update(0, 0, Count - 1, idx, value);

    private void Update(int node, int start, int end, int idx, T value)
    {
        if (start == end)
        {
            _data[idx] = value;
            _tree[node] = value;
        }
        else
        {
            int mid = (start + end) / 2;
            int leftChild = 2 * node + 1;
            int rightChild = 2 * node + 2;

            if (start <= idx && idx <= mid)
            {
                Update(leftChild, start, mid, idx, value);
            }
            else
            {
                Update(rightChild, mid + 1, end, idx, value);
            }

            _tree[node] = _operation(_tree[leftChild], _tree[rightChild]);
        }
    }

    public T Query(int left, int right) 
        => Query(0, 0, Count - 1, left, right);

    private T Query(int node, int start, int end, int left, int right)
    {
        if (right < start || end < left)
        {
            return _identityElement;
        }

        if (left <= start && end <= right)
        {
            return _tree[node];
        }

        int mid = (start + end) / 2;
        int leftChild = 2 * node + 1;
        int rightChild = 2 * node + 2;

        T leftResult = Query(leftChild, start, mid, left, right);
        T rightResult = Query(rightChild, mid + 1, end, left, right);

        return _operation(leftResult, rightResult);
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _data)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
