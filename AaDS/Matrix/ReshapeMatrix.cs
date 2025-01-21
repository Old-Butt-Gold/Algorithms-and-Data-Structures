namespace AaDS.Matrix;

public class ReshapeMatrix
{
    /// <summary>
    /// https://leetcode.com/problems/reshape-the-matrix/
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="r"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public int[][] MatrixReshape(int[][] mat, int r, int c)
    {
        int m = mat.Length;
        int n = mat[0].Length;

        if (m * n != r * c) return mat;

        var result = new int[r][];
        for (int i = 0; i < r; i++)
        {
            result[i] = new int[c];
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int index = i * n + j;
                result[index / c][index % c] = mat[i][j];
            }
        }

        return result;
    }
}