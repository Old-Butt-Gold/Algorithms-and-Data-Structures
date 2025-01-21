namespace AaDS.Intervals;

public static class NonOverlappingIntervals
{
    /// <summary>
    /// https://leetcode.com/problems/non-overlapping-intervals/description/?envType=study-plan-v2
    /// </summary>
    /// <param name="intervals"></param>
    /// <returns></returns>
    public static int EraseOverlapIntervals(int[][] intervals)
    {
        if (intervals.Length < 2) return 0;

        Array.Sort(intervals, (a, b) => a[1].CompareTo(b[1]));

        int count = 0;
        // [2, 3], [1, 3], [4, 5]
        //take end as '3'
        int end = intervals[0][1];

        for (int i = 1; i < intervals.Length; i++)
        {
            // '1' < '3'
            if (intervals[i][0] < end)
            {
                count++;
            }
            else
            {
                end = intervals[i][1];
            }
        }

        return count;
    }
}