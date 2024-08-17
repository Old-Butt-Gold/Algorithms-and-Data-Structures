namespace AaDS.Kadane;

public class MaximumSubarray
{
    /// <summary>
    /// Given an integer array nums, find the subarray with the largest sum, and return its sum.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MaxSubArray(int[] nums)
    {
        int bestSum = int.MinValue;
        int currentSum = 0;

        foreach (var num in nums)
        {
            currentSum = Math.Max(num, num + currentSum);
            bestSum = Math.Max(bestSum, currentSum);
        }

        return bestSum;
    }
}