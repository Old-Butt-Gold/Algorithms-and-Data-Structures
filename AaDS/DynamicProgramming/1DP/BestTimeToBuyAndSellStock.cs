﻿namespace AaDS.DynamicProgramming._1DP;

public partial class BestTimeToBuyAndSellStock
{
    /// <summary>
    /// You are given an array prices where prices[i] is the price of a given stock on the ith day.
    /// Find the maximum profit you can achieve. You may complete at most two transactions.
    /// Note: You may not engage in multiple transactions simultaneously (i.e., you must sell the stock before you buy again).
    /// </summary>
    /// <param name="prices"></param>
    /// <returns></returns>
    public static int MaxProfitIII(int[] prices)
    {
        if (prices.Length == 0) return 0;

        int countStocks = 2;
        
        var dp = new int[countStocks + 1];
        var min = new int[countStocks + 1];
        Array.Fill(min, prices[0]);
        
        
        for (int i = 1; i < prices.Length; i++)
        {
            for (int k = 1; k <= countStocks; k++)
            {
                min[k] = Math.Min(min[k], prices[i] - dp[k - 1]);
                dp[k] = Math.Max(dp[k], prices[i] - min[k]);
            }
        }

        return dp[countStocks];
    }

    /// <summary>
    /// You are given an integer array prices where prices[i] is the price of a given stock on the ith day, and an integer k.
    /// Find the maximum profit you can achieve. You may complete at most k transactions: i.e. you may buy at most k times and sell at most k times.
    /// Note: You may not engage in multiple transactions simultaneously (i.e., you must sell the stock before you buy again).
    /// </summary>
    /// <param name="k"></param>
    /// <param name="prices"></param>
    /// <returns></returns>
    public int MaxProfitIV(int k, int[] prices)
    {
        if (prices.Length == 0) return 0;

        var dp = new int[k + 1];
        var min = new int[k + 1];
        Array.Fill(min, prices[0]);


        for (int i = 1; i < prices.Length; i++)
        {
            for (int j = 1; j <= k; j++)
            {
                min[j] = Math.Min(min[j], prices[i] - dp[j - 1]);
                dp[j] = Math.Max(dp[j], prices[i] - min[j]);
            }
        }

        return dp[k];
    }

}