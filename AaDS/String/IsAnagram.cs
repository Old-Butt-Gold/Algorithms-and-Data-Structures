namespace AaDS.String;

public static class IsAnagram
{
    /// <summary>
    /// Given two strings s and t, return true if t is an anagram of s, and false otherwise.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool Anagram(string s, string t)
    {
        if (s.Length != t.Length) return false;

        int[] alphabet = new int[26];
        foreach (var item in s)
        {
            alphabet[item - 'a']++;
        }

        foreach (var item in t)
        {
            if (alphabet[item - 'a'] > 0)
            {
                alphabet[item - 'a']--;
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}