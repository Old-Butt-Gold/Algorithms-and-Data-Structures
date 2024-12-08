namespace AaDS.SlidingWindow;

public static class MaxVowelsSubstring
{
    /// <summary>
    /// Given a string s and an integer k, return the maximum number of vowel letters in any substring of s with length k.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int MaxVowels(string s, int k) 
    {
        HashSet<char> set = ['a', 'e', 'i', 'o', 'u'];

        int counter = 0;
        for (int i = 0; i < k; i++)
        {
            counter += set.Contains(s[i]) ? 1 : 0;
        }

        int resultCounter = counter;

        for (int i = k; i < s.Length; i++) 
        {
            counter += set.Contains(s[i]) ? 1 : 0;
            counter -= set.Contains(s[i - k]) ? 1 : 0;

            resultCounter = Math.Max(counter, resultCounter);
        }

        return resultCounter;
    }
}