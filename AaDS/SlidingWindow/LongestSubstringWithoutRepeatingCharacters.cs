namespace AaDS.SlidingWindow;

public class LongestSubstringWithoutRepeatingCharacters
{
    /// <summary>
    /// Given a string s, find the length of the longest substring without repeating characters.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int LengthOfLongestSubstring(string s)
    {
        if (s.Length is 0) return 0;

        var letters = new HashSet<char>();
        int max = 0;

        int left = 0;
        int right = 0;
        while (right < s.Length)
        {
            if (letters.Add(s[right]))
            {
                right++;
                max = Math.Max(max, letters.Count);
            }
            else
            {
                letters.Remove(s[left]);
                left++;
            }
        }

        return max;
    }
}