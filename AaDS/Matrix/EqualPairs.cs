namespace AaDS.Matrix;

public class EqualPairs
{
    /// <summary>
    /// Given a 0-indexed n x n integer matrix grid.
    /// A row and column pair is considered equal if they contain the same elements in the same order (i.e., an equal array).
    /// </summary>
    /// <param name="grid"></param>
    /// <returns>The number of pairs (ri, cj) such that row ri and column cj are equal.</returns>
    public int EqualPairsGrid(int[][] grid)
    {
        var rowHash = new Dictionary<long, int>();
        var colHash = new Dictionary<long, int>();

        for (int i = 0; i < grid.Length; i++)
        {
            long rowKey = ComputeHash(grid[i]);
            rowHash[rowKey] = rowHash.GetValueOrDefault(rowKey, 0) + 1;

            long colKey = ComputeColumnHash(grid, i);
            colHash[colKey] = colHash.GetValueOrDefault(colKey, 0) + 1;
        }

        int result = 0;
        foreach (var (key, count) in rowHash)
        {
            if (colHash.TryGetValue(key, out var colCount))
            {
                result += count * colCount;
            }
        }

        return result;
        
        long ComputeHash(int[] array)
        {
            long hash = 0;
            foreach (var num in array)
            {
                hash = HashCode.Combine(hash, num);
            }

            return hash;
        }

        long ComputeColumnHash(int[][] grid, int colIndex)
        {
            long hash = 0;
            foreach (var item in grid)
            {
                hash = HashCode.Combine(hash, item[colIndex]);
            }

            return hash;
        }
    }
}