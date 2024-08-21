namespace AaDS.Greedy;

public partial class BestTimeToBuyAndSellStock
{
    /// <summary>
    /// You are given an array prices where prices[i] is the price of a given stock on the ith day.
    /// You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.
    /// Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.
    /// </summary>
    /// <param name="prices"></param>
    /// <returns></returns>
    public static int MaxProfitI(int[] prices)
    {
        int max = 0;
        int min = prices[0];

        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] < min)
            {
                min = prices[i];
            }
            else if (prices[i] - min > max)
            {
                max = prices[i] - min;
            }

        }

        return max;
    }

    /// <summary>
    /// You are given an integer array prices where prices[i] is the price of a given stock on the ith day.
    /// On each day, you may decide to buy and/or sell the stock. You can only hold at most one share of the stock at any time. However, you can buy it then immediately sell it on the same day.
    /// Find and return the maximum profit you can achieve.
    /// </summary>
    /// <param name="prices"></param>
    /// <returns></returns>
    public static int MaxProfitII(int[] prices)
    {
        int sum = 0;
        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] > prices[i - 1])
            {
                sum += prices[i] - prices[i - 1];
            }
        }

        return sum;
    }
}