namespace AaDS.Intervals;

public class SummaryIntervals
{
    /// <summary>
    /// You are given a sorted unique integer array nums.
    /// A range [a,b] is the set of all integers from a to b (inclusive).
    /// Return the smallest sorted list of ranges that cover all the numbers in the array exactly. That is, each element of nums is covered by exactly one of the ranges, and there is no integer x such that x is in one of the ranges but not in nums.
    ///Each range [a,b] in the list should be output as:
    /// "a->b" if a != b;
    /// "a" if a == b.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public IList<string> SummaryRanges(int[] nums)
    {
        var result = new List<string>();
        for (int i = 0; i < nums.Length; i++)
        {
            int start = nums[i];
            while (i < nums.Length - 1 && nums[i + 1] - nums[i] == 1)
            {
                i++;
            }

            result.Add(start != nums[i] ? $"{start}->{nums[i]}" : $"{start}");
        }

        return result;
    }
}