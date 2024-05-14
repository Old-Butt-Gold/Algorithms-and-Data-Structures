namespace AaDS.DataStructures.Tree;

class FenwickTree
{
    int[] BIT;
    public int Count { get; }

    public FenwickTree(IEnumerable<int> values)
    {
        Count = values.Count();
        BIT = new int[Count + 1];
        int index = 0;

        foreach (var value in values)
            Update(index++, value);
    }

    public void UpdateAbsolute(int index, int newValue)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentException("Invalid index.");
        Update(index, newValue - RangeQuery(index, index));
    }

    
    void Update(int index, int delta)
    {
        index++;
        while (index <= Count)
        {
            BIT[index] += delta;
            index += index & -index;
        }
    }

    int Query(int index)
    {
        index++;
        int sum = 0;
        while (index > 0)
        {
            sum += BIT[index];
            index -= index & -index;
        }
        return sum;
    }

    public int RangeQuery(int start, int end)
    {
        if (start > end || start < 0 || end >= Count)
            throw new ArgumentException("Invalid range");
        return Query(end) - (start > 0 ? Query(start - 1) : 0);
    }
}