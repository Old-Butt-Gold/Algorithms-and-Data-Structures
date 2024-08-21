namespace AaDS.DynamicProgramming.MultiDimensionalDP;

public class UniquePaths
{
    /// <summary>
    /// https://leetcode.com/problems/unique-paths/
    /// </summary>
    /// <param name="m"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int UniquePathsI(int m, int n)
    {
        int rows = m;
        int cols = n;

        int[,] dp = new int[rows, cols];
        dp[0, 0] = 1;

        for (int i = 1; i < rows; i++)
        {
            //нет препятствия
            dp[i, 0] = dp[i - 1, 0];

        }

        for (int i = 1; i < cols; i++)
        {

            //нет препятствия
            dp[0, i] = dp[0, i - 1];

        }

        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < cols; j++)
            {

                dp[i, j] = dp[i - 1, j] + dp[i, j - 1];

            }
        }

        return dp[rows - 1, cols - 1];
    }

    /// <summary>
    /// https://leetcode.com/problems/unique-paths-ii/description/?envType=study-plan-v2&envId=top-interview-150
    /// </summary>
    /// <param name="obstacleGrid"></param>
    /// <returns></returns>
    public int UniquePathsWithObstaclesII(int[][] obstacleGrid)
    {
        int rows = obstacleGrid.Length;
        int cols = obstacleGrid[0].Length;

        if (obstacleGrid[0][0] == 1 || obstacleGrid[rows - 1][cols - 1] == 1)
        {
            return 0;
        }

        int[,] dp = new int[rows, cols];
        dp[0, 0] = 1;

        for (int i = 1; i < rows; i++)
        {
            if (obstacleGrid[i][0] == 0)
            {
                //нет препятствия
                dp[i, 0] = dp[i - 1, 0];
            }
        }

        for (int i = 1; i < cols; i++)
        {
            if (obstacleGrid[0][i] == 0)
            {
                //нет препятствия
                dp[0, i] = dp[0, i - 1];
            }
        }

        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < cols; j++)
            {
                if (obstacleGrid[i][j] == 0)
                {
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }
        }

        return dp[rows - 1, cols - 1];
    }
}