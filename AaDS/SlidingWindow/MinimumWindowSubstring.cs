namespace AaDS.SlidingWindow;

public class MinimumWindowSubstring
{
    /// <summary>
    /// Given two strings s and t of lengths m and n respectively, return the minimum window substring
    /// of s such that every character in t (including duplicates) is included in the window. If there is no such substring, return the empty string "".
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static string MinWindow(string s, string t)
    {
        if (s.Length < t.Length) return "";

        int[] map = new int[128];
        int count = t.Length;
        int start = 0;
        int end = 0;
        int minLen = int.MaxValue;
        int startIndex = 0;

        foreach (var c in t)
        {
            map[c]++;
        }

        while (end < s.Length)
        {
            map[s[end]]--;

            if (map[s[end]] >= 0)
            {
                count--;
            }

            while (count <= 0)
            {
                if (end - start < minLen)
                {
                    startIndex = start;
                    minLen = end - start;
                }

                if (map[s[start++]]++ == 0)
                {
                    count++;
                }
            }
        }

        return minLen == int.MaxValue ? "" : s.Substring(startIndex, minLen);
    }
}