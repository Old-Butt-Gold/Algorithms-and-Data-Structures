namespace AaDS.Matrix;

public static class TicTacToe
{
    static int[][] _winCombinations =
    [
        [0, 1, 2], [3, 4, 5], [6, 7, 8], // verticals
        [0, 4, 8], [2, 4, 6], // diagonals
        [0, 3, 6], [1, 4, 7], [2, 5, 8] // horizontals
    ];
    
    public static string Tictactoe(int[][] moves)
    {
        if (moves.Length < 5)
        {
            return "Pending";
        }

        var x = new bool[9];
        var o = new bool[9];
        bool xMove = true;

        foreach (var move in moves)
        {
            var index = move[0] + move[1] * 3;

            if (xMove)
            {
                x[index] = true;
            }
            else
            {
                o[index] = true;
            }
            xMove = !xMove;
        }

        foreach (var combination in _winCombinations)
        {
            if (combination.All(i => x[i]))
            {
                return "A";
            }

            if (combination.All(i => o[i]))
            {
                return "B";
            }
        }

        return moves.Length == 9 ? "Draw" : "Pending";
    }
}