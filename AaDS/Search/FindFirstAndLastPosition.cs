namespace AaDS.Search;

public class FindFirstAndLastPosition
{
    /// <summary>
    /// Given an array of integers nums sorted in non-decreasing order, find the starting and ending position of a given target value.
    /// If target is not found in the array, return [-1, -1].
    /// You must write an algorithm with O(log n) runtime complexity.
    ///</summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int[] SearchRange(int[] nums, int target)
    {
        int first = FindFirst(nums, target);
        int last = FindLast(nums, target); 
        return [first, last];
        
        static int FindFirst(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1; 
            int first = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2; // to avoid overflow of int

                if (nums[mid] == target)
                {
                    first = mid;
                    right = mid - 1;
                }
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return first;
        }
        
        static int FindLast(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            int last = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2; // to avoid overflow of int

                if (nums[mid] == target)
                {
                    last = mid;
                    left = mid + 1;
                }
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return last;
        }
    }
    
}