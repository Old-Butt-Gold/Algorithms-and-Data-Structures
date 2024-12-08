namespace AaDS.SlidingWindow;

public static class MaxConsecutiveOnes
{
    /// <summary>
    /// Given a binary array nums and an integer k, return the maximum number of consecutive 1's in the array if you can flip at most k 0's.
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int LongestOnes(int[] nums, int k)
    {
        int left = 0;
        int maxLength = 0;

        for (int right = 0; right < nums.Length; right++)
        {
            if (nums[right] == 0)
            {
                k--;
            }

            while (k < 0)
            {
                if (nums[left] == 0)
                {
                    k++;
                }

                left++;
            }

            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }
}