namespace AaDS.Matrix;

public class RotateImage
{
    /// <summary>
    /// You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees (clockwise).
    /// </summary>
    /// <param name="matrix"></param>
    public static void RotateClockwise(int[][] matrix)
    {
        int length = matrix.Length;
        Transpose(matrix, length);
        ReflectRow(matrix, length);
    }

    /// <summary>
    /// You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees (counterclockwise).
    /// </summary>
    /// <param name="matrix"></param>
    public static void RotateCounterClockwise(int[][] matrix)
    {
        int length = matrix.Length;
        Transpose(matrix, length);
        ReflectColumn(matrix, length);
    }

    static void Transpose(int[][] matrix, int length) //Transpose above main diagonal
    {
        for (int row = 0; row < length; row++)
        {
            for (int col = row; col < length; col++)
            {
                (matrix[col][row], matrix[row][col]) = (matrix[row][col], matrix[col][row]);
            }
        }
    }

    static void ReflectRow(int[][] matrix, int length) //Reverse each Row
    {
        for (int row = 0; row < length; row++)
        {
            for (int col = 0; col < length / 2; col++)
            {
                (matrix[row][col], matrix[row][length - col - 1]) = (matrix[row][length - col - 1], matrix[row][col]);
            }
        }
    }
    
    static void ReflectColumn(int[][] matrix, int length) //Reverse each column
    {
        for (int col = 0; col < length; col++)
        {
            for (int row = 0; row < length / 2; row++)
            {
                (matrix[row][col], matrix[length - row - 1][col]) = (matrix[length - row - 1][col], matrix[row][col]);
            }
        }
    }
}