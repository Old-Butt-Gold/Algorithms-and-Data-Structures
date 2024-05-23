namespace AaDS.Arrays;

public static class TwoSum
{
    /// <summary>
    /// Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
    /// You may assume that each input would have exactly one solution, and you may not use the same element twice.
    /// You can return the answer in any order.
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int[] TwoSumI(int[] nums, int target)
    {
        Dictionary<int, int> dictionary = new();

        for (int i = 0; i < nums.Length; i++)
        {
            int result = target - nums[i];

            if (dictionary.TryGetValue(result, out var value))
            {
                return [value, i];
            }

            if (!dictionary.ContainsKey(nums[i]))
            {
                dictionary.Add(nums[i], i);
            }
        }

        return null;
    }
    
    /// <summary>
    /// Input Array is sorted in non-decreasing order
    /// </summary>
    /// <param name="numbers"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int[] TwoSumII(int[] numbers, int target) {
        int left = 0;
        int right = numbers.Length - 1;

        while (left < right)
        {
            var sum = numbers[left] + numbers[right];
            if (sum == target) return [left, right];
            else if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }
        }
        
        return null;
    }
    
    
}