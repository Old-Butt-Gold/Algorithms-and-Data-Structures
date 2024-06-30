namespace AaDS.DynamicProgramming.MultiDimensionalDP;

public class MaximalSquare
{
    /// <summary>
    /// Given an m x n binary matrix filled with 0's and 1's, find the largest square containing only 1's and return its area.
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public int MaximalSquareI(char[][] matrix) {

        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int[,] result = new int[rows + 1, cols + 1];

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                result[i + 1, j + 1] = matrix[i][j] == '1' ? 1 : 0;
            }
        }

        int max = 0;
        for (int i = 1; i <= rows; i++) {
            for (int j = 1; j <= cols; j++) {
                if (matrix[i - 1][j - 1] == '1')
                    result[i, j] = Math.Min(Math.Min(result[i - 1, j], result[i, j - 1]), result[i - 1, j - 1]) + 1;
                max = Math.Max(max, result[i, j]);
            }
        }

        return max * max;
    }
}