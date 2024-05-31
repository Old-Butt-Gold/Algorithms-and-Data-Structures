namespace AaDS.DynamicProgramming._1DP;

public static class Robbery
{
    /// <summary>
    /// You are a professional robber planning to rob houses along a street. Each house has a certain amount of money stashed, the only constraint stopping you from robbing each of them is that adjacent houses have security systems connected and it will automatically contact the police if two adjacent houses were broken into on the same night.
    /// Given an integer array nums representing the amount of money of each house, return the maximum amount of money you can rob tonight without alerting the police.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int Rob(int[] nums)
    {
        if (nums.Length == 0) return 0;
        if (nums.Length < 3) return nums.Max();
    
        int[] money = new int[nums.Length];
        money[0] = nums[0];
        money[1] = Math.Max(nums[0], nums[1]);
    
        for (int i = 2; i < nums.Length; i++) {
            int ifLoot = money[i - 2] + nums[i];
            int ifNotLoot = money[i - 1];
    
            money[i] = Math.Max(ifLoot, ifNotLoot);
        }
    
        return money[^1];
    }
}