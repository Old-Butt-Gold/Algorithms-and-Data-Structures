namespace AaDS.DynamicProgramming._1DP;

public class LongestIncreasingSubsequence
{
    /// <summary>
    /// Given an integer array nums, return the length of the longest strictly increasing subsequence
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int LISDynamicProgramming(int[] nums) //1 DP O(n^2) and O(n) time and space comp
    {
        int[] dp = new int[nums.Length];
        Array.Fill(dp, 1);

        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (nums[j] < nums[i])
                {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }
        }

        return dp.Max();
    }

    public int LengthOfLISBinarySearch(int[] nums) // O(nlog(n)) and O(n) time and space comp
    {
        if (nums.Length == 0) return 0;

        List<int> dp = [];

        foreach (var num in nums)
        {
            int left = 0, right = dp.Count;

            // Бинарный поиск места для вставки текущего элемента num
            while (left < right)
            {
                int mid = (right + left) / 2;
                if (dp[mid] < num)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }

            // Если left указывает на конец списка, добавляем новый элемент
            if (left == dp.Count)
            {
                dp.Add(num);
            }
            else
            {
                // Иначе заменяем существующий элемент
                dp[left] = num;
            }
        }

        return dp.Count;
    }

}