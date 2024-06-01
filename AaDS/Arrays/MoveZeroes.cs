namespace AaDS.Arrays;

public static class MoveZeroes
{
    /// <summary>
    /// Given an integer array nums, move all 0's to the end of it while maintaining the relative order of the non-zero elements.
    /// Note that you must do this in-place without making a copy of the array.
    /// </summary>
    /// <param name="nums"></param>
    public static void MoveZeroesI(int[] nums) {
        int left = 0;

        for (int right = 0; right < nums.Length; right++) {
            if (nums[right] != 0) {
                (nums[left], nums[right]) = (nums[right], nums[left]);
                left++;
            }
        }
    }
}