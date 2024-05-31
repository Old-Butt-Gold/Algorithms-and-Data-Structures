namespace AaDS.Matrix;

public static class BinarySearchMatrix
{
    /// <summary>
    /// You are given an m x n integer matrix with the following two properties:
    /// Each row is sorted in non-decreasing order.
    /// The first integer of each row is greater than the last integer of the previous row.
    /// Given an integer target, return true if target is in matrix or false otherwise.
    /// You must write a solution in O(log(m * n)) time complexity.
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool SearchMatrix(int[][] matrix, int target)
    {
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int left = 0;
        int right = rows * cols - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int value = matrix[mid / cols][mid % cols];

            if (value == target)
            {
                return true;
            }
            else if (value < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return false;
    }

    static bool SearchMatrixWithoutDivision(int[][] matrix, int target)
    {
        int top = 0;
        int down = matrix.Length - 1;
        while (top <= down)
        {
            int mid = (top + down) / 2;
            int left = 0;
            int right = matrix[mid].Length - 1;
            while (left <= right)
            {
                int midRow = (left + right) / 2;
                if (matrix[mid][midRow] == target)
                {
                    return true;
                }
                else if (target < matrix[mid][midRow])
                {
                    right = midRow - 1;
                }
                else
                {
                    left = midRow + 1;
                }
            }

            if (target < matrix[mid][0])
            {
                down = mid - 1;
            }
            else
            {
                top = mid + 1;
            }

        }

        return false;
    }
}