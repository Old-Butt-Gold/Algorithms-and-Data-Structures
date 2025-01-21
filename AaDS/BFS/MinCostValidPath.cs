namespace AaDS.BFS;

public class MinCostValidPath
{
    // directions: right-1, left-2, down-3, up-4
    private static readonly int[][] Dirs = new int[][]
    {
        [0, 1],
        [0, -1],
        [1, 0],
        [-1, 0]
    };
    
    /// <summary>
    /// https://leetcode.com/problems/final-value-of-variable-after-performing-operations/
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static int MinCost(int[][] grid)
    {
        int numRows = grid.Length;
        int numCols = grid[0].Length;

        // Track minimum cost to reach each cell
        var minCost = new int[numRows][];
        for (int i = 0; i < numRows; i++)
        {
            minCost[i] = new int[numCols];
            Array.Fill(minCost[i], int.MaxValue);
        }

        // Use deque for 0-1 BFS - add zero cost moves to front, cost = 1 to back
        var deque = new LinkedList<int[]>();
        deque.AddFirst([0, 0]);
        minCost[0][0] = 0;

        while (deque.Count > 0)
        {
            var curr = deque.First!.Value;
            deque.RemoveFirst();
            int row = curr[0];
            int col = curr[1];

            for (int dir = 0; dir < 4; dir++)
            {
                int newRow = row + Dirs[dir][0];
                int newCol = col + Dirs[dir][1];

                int cost = grid[row][col] - 1 != dir ? 1 : 0;

                if (IsValid(newRow, newCol, numRows, numCols) &&
                    minCost[row][col] + cost < minCost[newRow][newCol])
                {

                    minCost[newRow][newCol] = minCost[row][col] + cost;

                    // Add to back if cost = 1, front if cost = 0
                    if (cost == 1)
                    {
                        deque.AddLast([newRow, newCol]);
                    }
                    else
                    {
                        deque.AddFirst([newRow, newCol]);
                    }
                }
            }
        }

        return minCost[^1][^1];
        
        // Check if coordinate is located inside bounds
        static bool IsValid(int row, int col, int numRows, int numCols) 
            => row >= 0 && row < numRows && col >= 0 && col < numCols;
    }

    
}