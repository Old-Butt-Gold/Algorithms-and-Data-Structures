namespace AaDS.Search;

public class FindMinRotatedSortedArray
{
    /// <summary>
    /// Given the sorted rotated array nums of unique elements, return the minimum element of this array.
    /// <remarks>
    /// You must write an algorithm that runs in O(log n) time.
    /// </remarks>
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int FindMin(int[] nums)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            int mid = (right + left) / 2;

            if (nums[mid] < nums[right])
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }

        return nums[left];
    }
}