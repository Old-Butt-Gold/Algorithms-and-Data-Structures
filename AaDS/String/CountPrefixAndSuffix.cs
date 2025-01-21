using System.Text;
using AaDS.DataStructures.Shared;
using AaDS.DataStructures.Tree;

namespace AaDS.String;

public static class CountPrefixAndSuffix
{
    /// <summary>
    /// https://leetcode.com/problems/count-prefix-and-suffix-pairs-i/?envType=daily-question
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public static int CountPrefixSuffixPairsI(string[] words)
    {
        int counter = 0;
        for (int i = 0; i < words.Length; i++)
        {
            for (int j = i + 1; j < words.Length; j++)
            {
                if (IsPrefixAndSuffix(words[i], words[j]))
                {
                    counter++;
                }
            }
        }

        return counter;

        bool IsPrefixAndSuffix(string str1, string str2) => str2.StartsWith(str1) && str2.EndsWith(str1);
    }

    /// <summary>
    /// https://leetcode.com/problems/count-prefix-and-suffix-pairs-ii/
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public static long CountPrefixSuffixPairsII(string[] words) {

        long counter = 0;
        TrieEnglishLetters trie = new();

        foreach (var word in words)
        {
            counter += trie.GetPrefixCount(word);
            trie.Add(word);
        }

        return counter;
    }
    
    static long GetPrefixCount(this TrieEnglishLetters englishLettersTrie, string word)
    {
        var current = englishLettersTrie.Root;
        long count = 0;
        StringBuilder sb = new();
        for (int i = 0; i < word.Length; i++) {
            sb.Append(word[i]);
            
            if (!current.Contains(word[i])) {
                break;
            }

            current = current.Next(word[i]);
            if (current!.IsEndOfWord && word.EndsWith(sb.ToString()))
            {
                count += current.WordCount;
            }
        }
        return count;
    }

}