namespace AaDS.Arrays;

public static class TrappingRainWater
{
    /// <summary>
    /// https://leetcode.com/problems/trapping-rain-water/
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public static int TrapI(int[] height)
    {
        if (height.Length is 0) return 0;

        int trappedWater = 0;

        int left = 0;
        int right = height.Length - 1;
        int maxLeft = height[left];
        int maxRight = height[right];

        while (left < right)
        {
            if (maxLeft <= maxRight)
            {
                left++;
                maxLeft = Math.Max(maxLeft, height[left]);
                trappedWater += Math.Max(0, maxLeft - height[left]);
            }
            else
            {
                right--;
                maxRight = Math.Max(maxRight, height[right]);
                trappedWater += Math.Max(0, maxRight - height[right]);
            }
        }

        return trappedWater;
    }

    private static readonly int[] Dx = [-1, 0, 1, 0];
    private static readonly int[] Dy = [0, 1, 0, -1];

    /// <summary>
    /// https://leetcode.com/problems/trapping-rain-water-ii/
    /// </summary>
    /// <param name="heightMap"></param>
    /// <returns></returns>
    public static int TrapRainWaterII(int[][]? heightMap)
    {
        if (heightMap == null || heightMap.Length == 0 || heightMap[0].Length == 0)
            return 0;

        int m = heightMap.Length;
        int n = heightMap[0].Length;

        var pq = new PriorityQueue<int[], int>();
        var visited = new bool[m, n];

        // Add border cells
        for (int j = 0; j < n; j++)
        {
            pq.Enqueue([heightMap[0][j], 0, j], heightMap[0][j]);
            pq.Enqueue([heightMap[m - 1][j], m - 1, j], heightMap[m - 1][j]);
            visited[0, j] = visited[m - 1, j] = true;
        }

        for (int i = 1; i < m - 1; i++)
        {
            pq.Enqueue([heightMap[i][0], i, 0], heightMap[i][0]);
            pq.Enqueue([heightMap[i][n - 1], i, n - 1], heightMap[i][n - 1]);
            visited[i, 0] = visited[i, n - 1] = true;
        }

        int water = 0;
        while (pq.Count > 0)
        {
            var curr = pq.Dequeue();
            int height = curr[0], row = curr[1], col = curr[2];

            for (int k = 0; k < 4; k++)
            {
                int newRow = row + Dx[k];
                int newCol = col + Dy[k];

                if (newRow < 0 || newRow >= m || newCol < 0 || newCol >= n || visited[newRow, newCol])
                    continue;

                if (heightMap[newRow][newCol] < height)
                {
                    water += height - heightMap[newRow][newCol];
                    pq.Enqueue([height, newRow, newCol], height);
                }
                else
                {
                    pq.Enqueue([heightMap[newRow][newCol], newRow, newCol], heightMap[newRow][newCol]);
                }

                visited[newRow, newCol] = true;
            }
        }

        return water;
    }
}