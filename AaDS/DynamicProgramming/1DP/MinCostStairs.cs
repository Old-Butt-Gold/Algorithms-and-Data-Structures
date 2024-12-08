namespace AaDS.DynamicProgramming._1DP;

public static class MinCostStairs
{
    /// <summary>
    /// You are given an integer array cost where cost[i] is the cost of ith step on a staircase. Once you pay the cost, you can either climb one or two steps.
    /// You can either start from the step with index 0, or the step with index 1.
    /// </summary>
    /// <param name="cost"></param>
    /// <returns>Return the minimum cost to reach the top of the floor.</returns>
    public static int MinCostClimbingStairs(IList<int> cost)
    {
        int[] dp = new int[cost.Count + 1];

        for (int i = 2; i <= cost.Count; i++)
        {
            dp[i] = Math.Min(dp[i - 1] + cost[i - 1], dp[i - 2] + cost[i - 2]);
        }

        return dp[cost.Count];
    }
}