namespace AaDS.Arrays;

public class IncreasingTripletSubsequence
{
    /// <summary>
    /// Given an integer array nums, return true if there exists a triple of indices (i, j, k)
    /// such that i < j < k and nums[i] < nums[j] < nums[k]. If no such indices exists, return false.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static bool IncreasingTriplet(int[] nums)
    {
        if (nums.Length < 3)
        {
            return false;
        }

        int smallest = int.MaxValue;
        int secondSmallest = int.MaxValue;

        foreach (int num in nums)
        {
            if (num <= smallest)
            {
                smallest = num;
            }
            else if (num <= secondSmallest)
            {
                secondSmallest = num;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}