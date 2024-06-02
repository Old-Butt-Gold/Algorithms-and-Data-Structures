using AaDS.Sortings;

namespace AaDS.Numerical;

public static class LargestPerimeterTriangle
{
    /// <summary>
    /// Given an integer array nums, return the largest perimeter of a triangle with a non-zero area, formed from three of these lengths. If it is impossible to form any triangle of a non-zero area, return 0.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int LargestPerimeter(int[] nums)
    {
        CountingSort.Sort(nums);
        for (int i = nums.Length - 1; i > 1; i--)
        {
            if (nums[i] < nums[i - 1] + nums[i - 2])
            {
                return nums[i] + nums[i - 1] + nums[i - 2];
            }
        }

        return 0;
    }
}