namespace AaDS.Kadane;

public class MaximumSubarraySumCircular
{
    /// <summary>
    /// https://leetcode.com/problems/maximum-sum-circular-subarray/description/?envType=study-plan-v2&envId=top-interview-150
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MaxSubarraySumCircular(int[] nums)
    {
        int total = 0;
        int max = int.MinValue;
        int currentMax = 0;

        int min = int.MaxValue;
        int currentMin = 0;

        foreach (var num in nums)
        {
            total += num;

            currentMax = Math.Max(num, currentMax + num);
            max = Math.Max(max, currentMax);

            currentMin = Math.Min(num, currentMin + num);
            min = Math.Min(min, currentMin);
        }

        return max > 0 ? Math.Max(max, total - min) : max;
    }
}