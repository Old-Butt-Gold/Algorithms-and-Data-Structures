namespace AaDS.Arrays;

public static class LongestConsecutiveSequence
{
    /// <summary>
    /// Given an unsorted array of integers nums, return the length of the longest consecutive elements sequence.
    /// You must write an algorithm that runs in O(n) time
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int LongestConsecutive(IList<int>? nums)
    {
        if (nums == null || nums.Count == 0)
        {
            return 0;
        }

        HashSet<int> numSet = [..nums];
        int longestStreak = 0;

        foreach (var num in numSet)
        {
            if (!numSet.Contains(num - 1))
            {
                int currentNum = num + 1;
                int currentStreak = 1;

                while (numSet.Contains(currentNum))
                {
                    currentNum++;
                    currentStreak++;
                }

                longestStreak = Math.Max(longestStreak, currentStreak);
            }
        }

        return longestStreak;
    }
}