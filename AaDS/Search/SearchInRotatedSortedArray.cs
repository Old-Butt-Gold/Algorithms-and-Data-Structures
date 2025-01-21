namespace AaDS.Search;

public static class SearchInRotatedSortedArray
{
    /// <summary>
    /// https://leetcode.com/problems/search-in-rotated-sorted-array/description/
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int Search(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2; // to avoid int overflow
            if (nums[mid] > nums[^1])
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        int answer = BinarySearch<int>.Search(nums, 0, left - 1, target);
        if (answer != -1)
        {
            return answer;
        }

        return BinarySearch<int>.Search(nums, left, nums.Length - 1, target);
    }
    
}