namespace AaDS.Intervals;

public class InsertInterval
{
    /// <summary>
    /// <para>
    /// You are given an array of non-overlapping intervals intervals where intervals[i] = [starti, endi] represent the start and the end of the ith interval and intervals is sorted in ascending order by starti. You are also given an interval newInterval = [start, end] that represents the start and end of another interval.
    /// Insert newInterval into intervals such that intervals is still sorted in ascending order by starti and intervals still does not have any overlapping intervals (merge overlapping intervals if necessary).
    /// </para>
    /// <para>
    /// Return intervals after the insertion.
    /// </para>
    /// <para>
    /// Note that you don't need to modify intervals in-place. You can make a new array and return it.
    /// </para>
    /// </summary>
    /// <param name="intervals"></param>
    /// <param name="newInterval"></param>
    /// <returns></returns>
    public static int[][] Insert(int[][] intervals, int[] newInterval)
    {
        List<int[]> result = [];

        int i = 0;
        while (i < intervals.Length && intervals[i][1] < newInterval[0])
        {
            result.Add(intervals[i++]);
        }

        while (i < intervals.Length && intervals[i][0] <= newInterval[1])
        {
            newInterval[0] = Math.Min(newInterval[0], intervals[i][0]);
            newInterval[1] = Math.Max(newInterval[1], intervals[i][1]);
            i++;
        }

        result.Add(newInterval);

        while (i < intervals.Length)
        {
            result.Add(intervals[i++]);
        }

        return result.ToArray();
    }
}