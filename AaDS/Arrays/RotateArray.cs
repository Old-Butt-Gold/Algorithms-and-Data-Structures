namespace AaDS.Arrays;

public class RotateArray
{
    /// <summary>
    /// Given an integer array nums, rotate the array to the right by k steps, where k is non-negative.
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    public static void Rotate(int[] nums, int k)
    {
        k %= nums.Length;

        if (k == 0) return;

        // Переворачиваем весь массив
        for (int i = 0; i < nums.Length / 2; i++)
        {
            (nums[i], nums[nums.Length - i - 1]) = (nums[nums.Length - i - 1], nums[i]);
        }

        // Переворачиваем первую часть до индекса k
        for (int i = 0; i < k / 2; i++)
        {
            (nums[i], nums[k - i - 1]) = (nums[k - i - 1], nums[i]);
        }

        // Переворачиваем оставшуюся часть массива от k до конца
        for (int i = k; i < (nums.Length + k) / 2; i++)
        {
            (nums[i], nums[nums.Length - i + k - 1]) =
                (nums[nums.Length - i + k - 1], nums[i]);
        }
    }
}