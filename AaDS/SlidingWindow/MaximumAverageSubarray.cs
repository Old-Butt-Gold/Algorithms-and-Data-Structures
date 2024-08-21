namespace AaDS.SlidingWindow;

public class MaximumAverageSubarray
{
    /// <summary>
    /// You are given an integer array nums consisting of n elements, and an integer k.
    /// Find a contiguous subarray whose length is equal to k that has the maximum average value and return this value. 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public double FindMaxAverage(int[] nums, int k)
    {
        int sum = 0;

        for (int i = 0; i < k; i++)
        {
            sum += nums[i];
        }

        double res = sum;

        for (int i = k; i < nums.Length; i++)
        {
            sum += nums[i] - nums[i - k];
            res = Math.Max(res, sum);
        }

        return res / k;
    }
}