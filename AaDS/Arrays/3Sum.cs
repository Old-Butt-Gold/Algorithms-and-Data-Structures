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
        List<IList<int>> result = [];
        if (nums.Length < 3) return result;
        Array.Sort(nums);
        int i = 0;
        while (i < nums.Length)
        {
            int target = -nums[i];
            //There is two Sum problem below
            int left = i + 1;
            int right = nums.Length - 1;
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
                    List<int> current = [nums[i], nums[left], nums[right]];
                    result.Add(current);
                    while (left < right && nums[left] == current[1])
                    {
                        left++;
                    }

                    while (left < right && nums[right] == current[2])
                    {
                        right--;
                    }
                }
            }

            int currentNumbers = nums[i];
            while (i < nums.Length && nums[i] == currentNumbers)
            {
                i++;
            }
        }

        return result;
    }
}