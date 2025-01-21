namespace AaDS.String;

public static class Construct
{
    /// <summary>
    /// Given two strings ransomNote and magazine,
    /// return true if ransomNote can be constructed by using
    /// the letters from magazine and false otherwise.
    /// </summary>
    /// <param name="ransomNote"></param>
    /// <param name="magazine"></param>
    public static bool CanConstructI(string ransomNote, string magazine) {
        Dictionary<char, int> dictionary = new();
        foreach (var item in magazine) {
            if (!dictionary.TryAdd(item, 1))
            {
                dictionary[item] += 1;
            }
        }

        foreach (var item in ransomNote) {
            if (!dictionary.ContainsKey(item)) return false;
            dictionary[item] -= 1;
            if (dictionary[item] == -1) {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// https://leetcode.com/problems/construct-k-palindrome-strings/description/?envType=daily-question
    /// Given a string s and an integer k
    /// </summary>
    /// <param name="s"></param>
    /// <param name="k"></param>
    /// <returns>true if you can use all the characters in s to construct k palindrome strings or false otherwise.</returns>
    public static bool CanConstructII(string s, int k)
    {
        if (s.Length < k) return false;
        if (s.Length == k) return true;

        int[] freq = new int[26];

        foreach (var ch in s)
        {
            freq[ch - 'a']++;
        }

        // Return if the number of odd frequencies is less than or equal to k
        return freq.Count(count => (count & 1) == 1) <= k;
    }
}