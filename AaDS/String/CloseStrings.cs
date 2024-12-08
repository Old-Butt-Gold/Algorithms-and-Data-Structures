namespace AaDS.String;

public static class CloseStrings
{
    /// <summary>
    /// https://leetcode.com/problems/determine-if-two-strings-are-close/description/?envType=study-plan-v2
    /// </summary>
    /// <param name="word1"></param>
    /// <param name="word2"></param>
    /// <returns></returns>
    public static bool CloseStringsI(string word1, string word2)
    {
        int[] freq1 = new int[26];
        int[] freq2 = new int[26];

        foreach (var ch in word1)
        {
            freq1[ch - 'a']++;
        }

        foreach (var ch in word2)
        {
            freq2[ch - 'a']++;
        }

        for (int i = 0; i < 26; i++)
        {
            if ((freq1[i] == 0 && freq2[i] != 0) || (freq1[i] != 0 && freq2[i] == 0))
            {
                return false;
            }
        }
        
        Array.Sort(freq1);
        Array.Sort(freq2);

        for (int i = 0; i < 26; i++) {
            if (freq1[i] != freq2[i]) {
                return false;
            }
        }
    
        return true;
    }
}