namespace AaDS.Matrix;

public class SpiralMatrix
{
    /// <summary>
    /// Given an m x n matrix, return all elements of the matrix in spiral order.
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static IList<int> SpiralOrder(int[][] matrix)
    {
        int rowCount = matrix.Length;
        int colCount = matrix[0].Length;
        List<int> result = [];
        int left = 0;
        int right = colCount - 1;
        int top = 0;
        int bottom = rowCount - 1;

        while (left <= right && top <= bottom)
        {
            for (int i = left; i <= right; i++)
            {
                result.Add(matrix[top][i]);
            }
            top++;

            for (int i = top; i <= bottom; i++)
            {
                result.Add(matrix[i][right]);
            }
            right--;

            if (top <= bottom)
            {
                for (int i = right; i >= left; i--)
                {
                    result.Add(matrix[bottom][i]);
                }
                bottom--;
            }

            if (left <= right)
            {
                for (int i = bottom; i >= top; i--)
                {
                    result.Add(matrix[i][left]);   
                }
                left++;
            }
        }
        
        return result;
    }
}