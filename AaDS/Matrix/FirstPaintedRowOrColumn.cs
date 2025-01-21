namespace AaDS.Matrix;

public static class FirstPaintedRowOrColumn
{
    /// <summary>
    /// https://leetcode.com/problems/first-completely-painted-row-or-column/description/?envType=daily-question
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="mat"></param>
    /// <returns></returns>
    public static int FirstCompleteIndex(int[] arr, int[][] mat)
    {
        int rows = mat.Length;
        int cols = mat[0].Length;

        // Create a map to store the positions of each number in mat
        Dictionary<int, (int row, int col)> positionMap = [];

        // Fill the map with the positions of the numbers in mat
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                positionMap[mat[i][j]] = (i, j);
            }
        }

        // Arrays to track painted rows and columns
        var rowPainted = new int[rows];
        var colPainted = new int[cols];

        for (int i = 0; i < arr.Length; i++)
        {
            var (row, col) = positionMap[arr[i]];

            // Paint the cell by incrementing the painted count for the row and column
            rowPainted[row]++;
            colPainted[col]++;

            // Check if a row or column is completely painted
            if (rowPainted[row] == cols || colPainted[col] == rows)
            {
                return i;
            }
        }

        // In case no row or column is fully painted (although the problem guarantees it will happen)
        return -1;
    }
}