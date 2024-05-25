namespace AaDS.Matrix;

public class ValidSudoku
{
    /// <summary>
    /// Determine if a 9 x 9 Sudoku board is valid. Only the filled cells need to be validated according to the following rules:
    /// 1. Each row must contain the digits 1-9 without repetition.
    /// 2. Each column must contain the digits 1-9 without repetition.
    /// 3. Each of the nine 3 x 3 sub-boxes of the grid must contain the digits 1-9 without repetition.
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public static bool IsValidSudoku(char[][] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            HashSet<char> row = [];
            HashSet<char> column = [];
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] != '.' && !row.Add(board[i][j])) return false;
                if (board[j][i] != '.' && !column.Add(board[j][i])) return false;
            }
        }

        //В каждом поле 3*3 нет повторяющихся цифры также
        for (int k = 0; k < 9; k++)
        {
            HashSet<char> box = [];
            int boxRow = k / 3 * 3;
            int boxCol = k % 3 * 3;
            for (int i = boxRow; i < boxRow + 3; i++)
            {
                for (int j = boxCol; j < boxCol + 3; j++)
                {
                    if (board[i][j] != '.' && !box.Add(board[i][j])) return false;
                }
            }
        }

        return true;
    }
}