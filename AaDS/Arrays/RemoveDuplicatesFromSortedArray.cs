namespace AaDS.Arrays;

public static class RemoveDuplicatesFromSortedArray
{
    /// <summary>
    /// Given an integer array nums sorted in non-decreasing order, remove the duplicates in-place such that each unique element appears only once.
    /// The relative order of the elements should be kept the same. Then return the number of unique elements in nums.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int RemoveDuplicates(int[] nums)
    {
        int offset = 1;
        int number = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            if (number != nums[i])
            {
                number = nums[i];
                nums[offset++] = number;
            }
        }

        return offset;
    }

    /// <summary>
    /// Given an integer array nums sorted in non-decreasing order, remove some duplicates in-place such that each unique element appears at most twice.
    /// The relative order of the elements should be kept the same.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int RemoveDuplicatesII(int[] nums)
    {
        int offset = 1;
        int number = nums[0];

        bool isDoubled = false;
        for (int i = 1; i < nums.Length; i++)
        {
            if (number == nums[i] && !isDoubled)
            {
                isDoubled = true;
                nums[offset++] = number;
                continue;
            }

            if (number != nums[i])
            {
                isDoubled = false;
                number = nums[i];
                nums[offset++] = number;
            }
        }

        return offset;
    }
}