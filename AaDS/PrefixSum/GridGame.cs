namespace AaDS.PrefixSum;

public static class GridGame
{
    /// <summary>
    /// https://leetcode.com/problems/grid-game/description/?envType=daily-question
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static long GridGameI(int[][] grid)
    {
        var firstRow = grid[0];
        var secondRow = grid[1];

        long firstRowSum = 0;

        foreach (var num in firstRow)
        {
            firstRowSum += num;
        }

        long secondRowSum = 0;
        long minSum = long.MaxValue;

        for (int turnIndex = 0; turnIndex < firstRow.Length; turnIndex++)
        {
            firstRowSum -= firstRow[turnIndex];
            minSum = Math.Min(minSum, Math.Max(firstRowSum, secondRowSum));
            secondRowSum += secondRow[turnIndex];
        }

        return minSum;
    }
}