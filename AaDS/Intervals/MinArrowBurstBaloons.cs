namespace AaDS.Intervals;

public static class MinArrowBurstBaloons
{
    /// <summary>
    /// https://leetcode.com/problems/minimum-number-of-arrows-to-burst-balloons/description/?envType=study-plan-v2
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static int FindMinArrowShots(int[][] points)
    {
        // Sort the balloons based on their end coordinates
        Array.Sort(points, (a, b) => a[1].CompareTo(b[1]));

        int arrows = 1;
        int prevEnd = points[0][1];

        // Count the number of non-overlapping intervals
        for (int i = 1; i < points.Length; i++)
        {
            if (prevEnd < points[i][0])
            {
                arrows++;
                prevEnd = points[i][1];
            }
        }

        return arrows;
    }
}