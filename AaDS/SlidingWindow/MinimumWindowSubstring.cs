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
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t) || s.Length < t.Length) return "";

        int[] targetCount = new int[128]; // Assuming ASCII characters

        foreach (char c in t)
        {
            targetCount[c]++;
        }

        int left = 0, right = 0;
        int minLen = int.MaxValue;
        int minStart = 0;
        int requiredChars = t.Length;

        while (right < s.Length)
        {
            if (targetCount[s[right++]]-- > 0)
            {
                requiredChars--;
            }

            while (requiredChars == 0)
            {
                if (right - left < minLen)
                {
                    minLen = right - left;
                    minStart = left;
                }

                if (targetCount[s[left++]]++ == 0)
                {
                    requiredChars++;
                }
            }
        }

        return (minLen == int.MaxValue) ? "" : s.Substring(minStart, minLen);
    }
}