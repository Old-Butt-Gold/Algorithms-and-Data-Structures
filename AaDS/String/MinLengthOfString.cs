namespace AaDS.String;

public static class MinLengthOfString
{
    /// <summary>
    /// https://leetcode.com/problems/minimum-length-of-string-after-operations/?envType=daily-question
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int MinimumLength(string s)
    {
        var freq = new int[26];

        foreach (var c in s)
        {
            freq[c - 'a']++;
        }

        int minLength = 0;

        foreach (var item in freq)
        {
            if (item == 0) continue;

            if (item % 2 == 0) minLength += 2;

            if (item % 2 == 1) minLength++;
        }

        return minLength;
    }
}