namespace AaDS.BFS;

public static class MapHighestPeak
{
    /// <summary>
    /// https://leetcode.com/problems/map-of-highest-peak/description/?envType=daily-question
    /// </summary>
    /// <param name="isWater"></param>
    /// <returns></returns>
    static int[][] HighestPeak(int[][] isWater)
    {
        int rows = isWater.Length;
        int cols = isWater[0].Length;

        int[][] heights = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            heights[i] = new int[cols];
        }

        int[][] directions =
        [
            [-1, 0],
            [1, 0],
            [0, -1],
            [0, 1]
        ];

        var queue = new Queue<int[]>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (isWater[i][j] == 1)
                {
                    heights[i][j] = 0;
                    queue.Enqueue([i, j]);
                }
                else
                {
                    heights[i][j] = -1;
                }
            }
        }

        while (queue.Count > 0)
        {
            var position = queue.Dequeue();
            int x = position[0];
            int y = position[1];

            foreach (var direction in directions)
            {
                int newX = x + direction[0];
                int newY = y + direction[1];

                if (newX > -1 && newX < rows && newY > -1 && newY < cols && heights[newX][newY] == -1)
                {
                    heights[newX][newY] = heights[x][y] + 1;
                    queue.Enqueue([newX, newY]);
                }
            }
        }

        return heights;
    }
}