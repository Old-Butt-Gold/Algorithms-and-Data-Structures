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
        Update(index, newValue - RangeQuery(index, index));
    }
    
    public void Update(int index, int delta)
    {
        index++;
        while (index <= Count)
        {
            BIT[index] += delta;
            index += index & -index;
        }
    }

    //Сумма от 0 до index
    public int Query(int index)
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

    public int RangeQuery(int left, int right) 
        => Query(right) - Query(left - 1);
}