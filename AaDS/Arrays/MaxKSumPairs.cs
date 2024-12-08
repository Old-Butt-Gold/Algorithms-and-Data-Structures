namespace AaDS.Arrays;

public static class MaxKSumPairs
{
    /// <summary>
    /// You are given an integer array nums and an integer k.
    /// In one operation, you can pick two numbers from the array whose sum equals k and remove them from the array.
    /// </summary>
    /// <param name="nums">Array of integers.</param>
    /// <param name="k">Target sum for each operation.</param>
    /// <returns>the maximum number of operations you can perform on the array.</returns>
    public static int MaxOperations(int[] nums, int k)
    {
        var map = new Dictionary<int, int>();
        int count = 0;

        foreach (var num in nums)
        {
            int target = k - num;
            if (map.TryGetValue(target, out int freq) && freq > 0)
            {
                count++;
                if (freq == 1)
                    map.Remove(target);
                else
                    map[target]--;
            }
            else
            {
                map[num] = map.GetValueOrDefault(num, 0) + 1;
            }
        }

        return count;
    }
}