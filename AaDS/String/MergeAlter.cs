using System.Text;

namespace AaDS.String;

public static class MergeAlter
{
    /// <summary>
    /// You are given two strings word1 and word2. Merge the strings by adding letters in alternating order, starting with word1. If a string is longer than the other, append the additional letters onto the end of the merged string.
    /// </summary>
    /// <param name="word1"></param>
    /// <param name="word2"></param>
    /// <returns>Return the merged string.</returns>
    public static string MergeAlternately(string word1, string word2)
    {
        StringBuilder sb = new();
        int i = 0;
        while (i < word1.Length && i < word2.Length)
        {
            sb.Append(word1[i]).Append(word2[i]);
            i++;
        }

        while (i < word1.Length)
        {
            sb.Append(word1[i]);
            i++;
        }

        while (i < word2.Length)
        {
            sb.Append(word2[i]);
            i++;
        }

        return sb.ToString();
    }
}