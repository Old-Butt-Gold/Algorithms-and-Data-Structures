namespace AaDS.Matrix;

public class DiagonalSum
{
    /// <summary>
    /// Given a square matrix mat, return the sum of the matrix diagonals.
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    public static int DiagonalSumI(int[][] mat)
    {
        int offset = 0, result = 0;

        foreach (var row in mat)
        {
            result += row[offset];
            offset++;
            result += row[^offset];
        }

        if (mat.Length % 2 == 1)
            result -= mat[mat.Length / 2][mat.Length / 2];

        return result;
    }
}