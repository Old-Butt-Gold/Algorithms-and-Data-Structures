namespace AaDS.Bits;

public static class SingleNumber
{
    /// <summary>
    /// Given a non-empty array of integers nums, every element appears twice except for one. Find that single one.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int SearchSingleNumber(int[] nums)
    {
        int num = 0;
        foreach (var item in nums)
        {
            num ^= item;
        }

        return num;
    }

    /// <summary>
    /// Given an integer array nums where every element appears three times except for one,
    /// which appears exactly once. Find the single element and return it.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int SingleNumberII(int[] nums)
    {
        int ones = 0; // Tracks the bits that have appeared once
        int twos = 0; // Tracks the bits that have appeared twice

        foreach (int num in nums)
        {
            ones = (ones ^ num) & ~twos;
            twos = (twos ^ num) & ~ones;
        }

        return ones;
    }
}