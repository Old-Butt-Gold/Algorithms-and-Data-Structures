namespace AaDS.DynamicProgramming.MultiDimensionalDP;

public class MinPath
{
    /// <summary>
    /// Given a m x n grid filled with non-negative numbers, find a path from top left to bottom right, which minimizes the sum of all numbers along its path.
    /// Note: You can only move either down or right at any point in time.
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static int MinPathSum(int[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        var dp = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            dp[i] = new int[cols];
        }

        dp[0][0] = grid[0][0];

        for (int i = 1; i < rows; i++)
        {
            dp[i][0] += dp[i - 1][0] + grid[i][0];
        }

        for (int j = 1; j < cols; j++)
        {
            dp[0][j] = dp[0][j - 1] + grid[0][j];
        }

        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < cols; j++)
            {
                dp[i][j] = grid[i][j] + Math.Min(dp[i - 1][j], dp[i][j - 1]);
            }
        }

        return dp[rows - 1][cols - 1];
    }
}