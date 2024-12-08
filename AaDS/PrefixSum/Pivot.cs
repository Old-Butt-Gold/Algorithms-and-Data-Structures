namespace AaDS.PrefixSum;

public class Pivot
{
    /// <summary>
    /// Given an array of integers nums, calculate the pivot index of this array.
    /// The pivot index is the index where the sum of all the numbers strictly to the left of the index is equal to the sum of all the numbers strictly to the index's right.
    /// If the index is on the left edge of the array, then the left sum is 0 because there are no elements to the left. This also applies to the right edge of the array.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns>The leftmost pivot index. If no such index exists, return -1.</returns>
    public int PivotIndex(int[] nums)
    {
        int sumRight = nums.Sum();
        int sumLeft = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            sumRight -= nums[i];

            if (sumLeft == sumRight)
            {
                return i;
            }

            sumLeft += nums[i];
        }

        return -1;
    }
}