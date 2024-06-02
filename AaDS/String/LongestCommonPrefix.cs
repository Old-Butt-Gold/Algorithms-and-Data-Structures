using AaDS.DataStructures.Tree;

namespace AaDS.String;

public static class LongestCommonPrefix
{
    /// <summary>
    /// Write a function to find the longest common prefix string amongst an array of strings.
    /// If there is no common prefix, return an empty string "".
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public static string Find(string[] strs)
    {
        Trie trie = new();
        foreach (var str in strs)
        {
            trie.Add(str);
        }

        return trie.GetLongestCommonPrefix();
    }
}