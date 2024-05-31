namespace AaDS.DynamicProgramming._1DP;

public static class Coins
{
    /// <summary>
    /// You are given an integer array coins representing coins of different denominations and an integer amount representing a total amount of money.
    /// Return the fewest number of coins that you need to make up that amount. If that amount of money cannot be made up by any combination of the coins, return -1.
    /// You may assume that you have an infinite number of each kind of coin.
    /// </summary>
    /// <param name="coins"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    
    //Why not greedy? Check for coins = [1, 6, 7, 9, 11] , amount = 13. With Greedy it would take 11 and two times 1. With DP – 6 and 7
    public static int CoinChange(int[] coins, int amount)
    {
        //coins may be not sorted
        int[] dp = new int[amount + 1];
        Array.Fill(dp, amount + 1);
        dp[0] = 0;

        for (int i = 1; i <= amount; i++)
        {
            foreach (var coin in coins)
            {
                if (i - coin > -1)
                {
                    dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                }
            }
        }

        return dp[amount] > amount ? -1 : dp[amount];
    }
}