namespace AaDS.SlidingWindow;

public class LongestSubbarrayOnes
{
    /// <summary>
    /// Given a binary array nums, you should delete one element from it.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns>Return the size of the longest non-empty subarray containing only 1's in the resulting array. Return 0 if there is no such subarray.</returns>
    public int LongestSubarray(int[] nums) {
        int left = 0;
        int maxLength = 0;
        int k = 1;

        for (int right = 0; right < nums.Length; right++)
        {
            if (nums[right] == 0)
            {
                k--;
            }

            while (k < 0)
            {
                if (nums[left] == 0)
                {
                    k++;
                }

                left++;
            }

            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength - 1;
    }
}