namespace AaDS.DataStructures.Tree;

class FenwickTree
{
    readonly int[] _tree;
    public int Count { get; }

    public FenwickTree(IList<int> values) : this(values.Count)
    {
        int index = 0;

        foreach (var value in values)
            Add(index++, value);
    }

    public FenwickTree(int count)
    {
        Count = count;
        _tree = new int[Count + 1];
    }

    public void Update(int index, int newValue)
    {
        int currentValue = SumRange(index, index);
        int delta = newValue - currentValue;
        Add(index, delta);
    }
    
    public void Add(int index, int delta)
    {
        index++;
        while (index < _tree.Length)
        {
            _tree[index] += delta;
            index += index & -index;
        }
    }
    
    public int FindIndexByPrefixSum(int targetSum)
    {
        if (Sum(Count - 1) < targetSum)
            throw new ArgumentOutOfRangeException(nameof(targetSum), "Target sum is greater than the total sum of all elements.");

        int index = 0;
        int currentSum = 0;
        int mask = HighestPowerOfTwo(Count);

        while (mask > 0)
        {
            int nextIndex = index + mask;
            if (nextIndex <= Count && currentSum + _tree[nextIndex] < targetSum)
            {
                index = nextIndex;
                currentSum += _tree[nextIndex];
            }
            mask >>= 1;
        }
        
        return index;
        
        int HighestPowerOfTwo(int x)
        {
            int power = 1;
            while (power <= x)
                power <<= 1;
            return power >> 1;
        }
    }
    
    public void Remove(int index) => Update(index, 0);

    public int Sum(int index)
    {
        index++;
        int sum = 0;
        while (index > 0)
        {
            sum += _tree[index];
            index -= index & -index;
        }
        return sum;
    }

    public int SumRange(int left, int right) 
        => Sum(right) - Sum(left - 1);
}