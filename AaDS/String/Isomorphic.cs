namespace AaDS.String;

public static class Isomorphic
{
    /// <summary>
    /// Given two strings s and t, determine if they are isomorphic.
    ///Two strings s and t are isomorphic if the characters in s can be replaced to get t.
    ///All occurrences of a character must be replaced with another character while preserving the order of characters. No two characters may map to the same character, but a character may map to itself.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool IsIsomorphic(string s, string t) {
        Dictionary<char,char> dict = new();
        HashSet<char> hSet = [];

        for(int i = 0; i < s.Length;i++)
        {
            if (!dict.TryGetValue(s[i], out var value))
            {
                if (hSet.Contains(t[i])) return false;
                dict.Add(s[i], t[i]);
                hSet.Add(t[i]);
            }
            else    //check if exists
            {
                if(t[i] != value) return false;
            }
        }

        return true;
    }
    
    /* worse for memory
     var dict1 = new Dictionary<char, char>();
        var dict2 = new Dictionary<char, char>();
        
        if (s.Length != t.Length) return false;

        for (int i = 0; i < s.Length; i++) {
            dict1.TryAdd(s[i], t[i]);
            dict2.TryAdd(t[i], s[i]);
            if (dict1[s[i]] != t[i]) return false;
            if (dict2[t[i]] != s[i]) return false;
        }
     */
}