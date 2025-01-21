namespace AaDS.Bits;

public static class XorAllPairings
{
    /// <summary>
    /// https://leetcode.com/problems/bitwise-xor-of-all-pairings/description/?envType=daily-question
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <returns></returns>
    public static int XorAllNums(int[] nums1, int[] nums2)
    {
        var numsxor = 0;

        if (nums1.Length % 2 == 0 && nums2.Length % 2 == 0)
        {
            return numsxor;
        }

        if (nums1.Length % 2 != 0 && nums2.Length % 2 != 0)
        {
            foreach (var num in nums1)
                numsxor ^= num;

            foreach (var num in nums2)
                numsxor ^= num;

            return numsxor;
        }

        if (nums1.Length % 2 == 0)
        {
            foreach (var num in nums1)
                numsxor ^= num;

            return numsxor;
        }

        if (nums2.Length % 2 == 0)
        {
            foreach (var num in nums2)
                numsxor ^= num;

            return numsxor;
        }

        return numsxor;
    }
}