namespace AaDS.String;

public static class AppendCharacters
{
    /// <summary>
    /// You are given two strings s and t consisting of only lowercase English letters.
    /// Return the minimum number of characters that need to be appended to the end of s so that t becomes a subsequence of s.
    /// A subsequence is a string that can be derived from another string by deleting some or no characters without changing the order of the remaining characters.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static int AppendCharactersI(string s, string t)
    {
        int sIndex = 0;
        int tIndex = 0;

        while (sIndex < s.Length && tIndex < t.Length)
        {
            if (s[sIndex] == t[tIndex])
            {
                tIndex++;
            }

            sIndex++;
        }

        return t.Length - tIndex;
    }
}