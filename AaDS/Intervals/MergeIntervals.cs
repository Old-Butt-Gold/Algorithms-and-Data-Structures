using Microsoft.VisualBasic.CompilerServices;

namespace AaDS.Intervals;

public class MergeIntervals
{
    /// <summary>
    /// Given an array of intervals where intervals[i] = [starti, endi], merge all overlapping intervals, and return an array of the non-overlapping intervals that cover all the intervals in the input.
    /// </summary>
    /// <param name="intervals"></param>
    /// <returns></returns>
    public static int[][] Merge(int[][] intervals)
    {
        if (intervals.Length < 2) return intervals;

        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        List<int[]> output = [intervals[0]];

        for (int i = 1; i < intervals.Length; i++)
        {
            if (output[^1][1] >= intervals[i][0])
            {
                if (output[^1][1] <= intervals[i][1])
                {
                    output[^1][1] = intervals[i][1];
                }
            }
            else
            {
                output.Add(intervals[i]);
            }
        }

        return output.ToArray();
    }
}