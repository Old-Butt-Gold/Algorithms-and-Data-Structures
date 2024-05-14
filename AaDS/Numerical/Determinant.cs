namespace AaDS.Numerical;

static class Determinant
{   
    static int CalculateDeterminant(int[,] matrix)
    {
        int size = matrix.GetLength(0);
        if (size != matrix.GetLength(1))
            throw new ArgumentException("Матрица не квадратная");

        if (size == 1)
            return matrix[0, 0];
        if (size == 2)
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        int determinant = 0;

        for (int j = 0; j < size; j++)
        {
            int[,] subMatrix = new int[size - 1, size - 1];
            for (int row = 1; row < size; row++)
            for (int col = 0, newCol = 0; col < size; col++)
            {
                if (col != j)
                    subMatrix[row - 1, newCol++] = matrix[row, col]; 
            }
            determinant += matrix[0, j] * (int)Math.Pow(-1, j) * CalculateDeterminant(subMatrix);
        }

        return determinant;
    }
}