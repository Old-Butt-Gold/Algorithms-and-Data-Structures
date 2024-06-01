namespace AaDS.Arrays;

public static class MonotomicArray
{
    /// <summary>
    /// An array is monotonic if it is either monotone increasing or monotone decreasing.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static bool IsMonotonic(int[] nums)
    {
        bool increasing = true;
        bool decreasing = true;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > nums[i - 1])
            {
                decreasing = false;
            }
            else if (nums[i] < nums[i - 1])
            {
                increasing = false;
            }

            if (!increasing && !decreasing)
            {
                return false;
            }
        }

        return true;
    }
}