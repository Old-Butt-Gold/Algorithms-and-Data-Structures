using Microsoft.VisualBasic.CompilerServices;

namespace AaDS.Intervals;

public class MergeIntervals
{
    /// <summary>
    /// Given an array of intervals where intervals[i] = [starti, endi], merge all overlapping intervals,
    /// and return an array of the non-overlapping intervals that cover all the intervals in the input.
    /// </summary>
    /// <param name="intervals"></param>
    /// <returns></returns>
    public static int[][] Merge(int[][] intervals)
    {
        if (intervals.Length < 2) return intervals;

        Array.Sort(intervals, (a, b) => a[0] - b[0]);
        LinkedList<int[]> merged = [];

        foreach (var interval in intervals)
        {
            // if the list of merged intervals is empty or if the current interval does not overlap with the previous, append it
            if (merged.Count == 0 || merged.Last!.Value[1] < interval[0])
            {
                merged.AddLast(interval);
            }
            // otherwise, there is overlap, so we merge the current and previous intervals
            else
            {
                merged.Last.Value[1] = Math.Max(merged.Last.Value[1], interval[1]);
            }
        }

        return merged.ToArray();
    }
}