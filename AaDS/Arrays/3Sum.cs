namespace AaDS.Arrays;

public class ThreeSum
{
    /// <summary>
    /// Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.
    /// Notice that the solution set must not contain duplicate triplets.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static IList<IList<int>> Sum3(int[] nums)
    {
        var result = new List<IList<int>>();
        Array.Sort(nums);
        for (int i = 0; i < nums.Length; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            //There is two Sum problem below
            int left = i + 1;
            int right = nums.Length - 1;
            int target = -nums[i];

            while (left < right)
            {
                if (nums[left] + nums[right] > target)
                {
                    right--;
                }
                else if (nums[left] + nums[right] < target)
                {
                    left++;
                }
                else
                {
                    result.Add([nums[i], nums[left], nums[right]]);
                    
                    // move left pointer, skipping duplicates
                    left++;
                    while (left < right && nums[left] == nums[left - 1])
                    {
                        left++;
                    }
                }
            }
        }

        return result;
    }
}