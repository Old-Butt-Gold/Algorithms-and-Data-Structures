namespace AaDS.DynamicProgramming._1DP;

public static class JumpGame
{
    /// <summary>
    /// You are given an integer array nums. You are initially positioned at the array's first index, and each element in the array represents your maximum jump length at that position.
    /// Return true if you can reach the last index, or false otherwise.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static bool CanJumpI(int[] nums)
    {
        int jumps = 0;
        foreach (var num in nums)
        {
            if (jumps < 0)
            {
                return false;
            }

            if (num > jumps)
            {
                jumps = num;
            }

            jumps--;
        }

        return true;
        
        /*
        int finishIndex = nums.Length - 1;
        for (int i = nums.Length - 1; i > -1; i--)
        {
            if (i + nums[i] >= finishIndex)
            {
                if (i == 0) return true;
                finishIndex = i;
            }
        }
        return false;
         */
    }

    /// <summary>
    /// You are given a 0-indexed array of integers nums of length n. You are initially positioned at nums[0].
    /// Each element nums[i] represents the maximum length of a forward jump from index i. In other words, if you are at nums[i], you can jump to any nums[i + j] where:
    /// 0 <= j <= nums[i] and
    /// i + j < n
    /// "return the minimum number of jumps to reach nums[n - 1]. The test cases are generated such that you can reach nums[n - 1]." />
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int Jump(int[] nums)
    {
        int jumps = 0;
        int currEnd = 0;
        int currFarthest = 0;
        for (int i = 0; i < nums.Length - 1; i++)
        {
            currFarthest = Math.Max(currFarthest, i + nums[i]);
            if (i == currEnd)
            {
                jumps++;

                currEnd = currFarthest;
            }
        }

        return jumps;
    }
}