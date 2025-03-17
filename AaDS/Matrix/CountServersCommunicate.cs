namespace AaDS.Matrix;

public static class CountServersCommunicate
{
    public static int CountServers(int[][] grid)
    {
        if (grid is null || grid.Length == 0) return 0;

        var rowCounts = new int[grid.Length];
        var colCounts = new int[grid[0].Length];

        for (int row = 0; row < grid.Length; row++)
        {
            for (int col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] == 1)
                {
                    rowCounts[row]++;
                    colCounts[col]++;
                }
            }
        }

        int communicableServersCount = 0;

        for (int row = 0; row < grid.Length; row++)
        {
            for (int col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] == 1)
                {
                    if (rowCounts[row] > 1 || colCounts[col] > 1)
                    {
                        communicableServersCount++;
                    }
                }
            }
        }

        return communicableServersCount;
    }
}