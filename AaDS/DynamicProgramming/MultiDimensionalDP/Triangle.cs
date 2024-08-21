namespace AaDS.DynamicProgramming.MultiDimensionalDP;

public class Triangle
{
    /// <summary>
    /// Given a triangle array, return the minimum path sum from top to bottom.
    /// For each step, you may move to an adjacent number of the row below. More formally, if you are on index i on the current row, you may move to either index i or index i + 1 on the next row.
    /// </summary>
    /// <param name="triangle"></param>
    /// <returns></returns>
    public static int MinimumTotal(IList<IList<int>> triangle)
    {
        if (triangle is null || triangle.Count == 0)
        {
            return 0;
        }

        int[] dp = new int[triangle.Count + 1];

        for (int row = triangle.Count - 1; row > -1; row--)
        {
            var currentRow = triangle[row];
            for (int i = 0; i < currentRow.Count; i++)
            {
                dp[i] = Math.Min(dp[i], dp[i + 1]) + currentRow[i];
            }
        }

        return dp[0];
    }
}