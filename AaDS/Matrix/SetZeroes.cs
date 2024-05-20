namespace AaDS.Matrix;

public class SetZeroes
{
    /// <summary>
    /// Given an m x n integer matrix, if an element is 0, set its entire row and column to 0's.
    /// </summary>
    /// <param name="matrix"></param>
    public void Set(int[][] matrix) {
        var linesToZero = new HashSet<int>();
        var colsToZero = new HashSet<int>();

        for(int i = 0; i < matrix.Length; i++)
        {
            for(int j = 0; j < matrix[i]. Length; j++)
            {
                if(matrix[i][j] == 0)
                {
                    linesToZero.Add(i); 
                    colsToZero.Add(j);
                }
            }
        }

        foreach(var lineToZero in linesToZero)
        {
            for (int j = 0; j < matrix[0].Length; j++)
            {
                matrix[lineToZero][j] = 0;
            } 
        }
        
        foreach(var colToZero in colsToZero)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i][colToZero] = 0;
            } 
        }
    }
}