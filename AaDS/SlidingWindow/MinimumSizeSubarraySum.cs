namespace AaDS.SlidingWindow;

public static class MinimumSizeSubarraySum
{
    /// <summary>
    /// Given an array of positive integers nums and a positive integer target
    /// </summary>
    /// <param name="target"></param>
    /// <param name="nums"></param>
    /// <returns>the minimal length of a subarray whose sum is greater than or equal to target.
    /// If there is no such subarray, return 0 instead.</returns>
    public static int MinSubArrayLen(int target, int[] nums)
    {
        int left = 0, right = 0, sumOfCurrentWindow = 0;
        int res = int.MaxValue;

        for (right = 0; right < nums.Length; right++) {
            sumOfCurrentWindow += nums[right];

            while (sumOfCurrentWindow >= target) {
                res = Math.Min(res, right - left + 1);
                sumOfCurrentWindow -= nums[left++];
            }
        }

        return res == int.MaxValue ? 0 : res;
    }
}