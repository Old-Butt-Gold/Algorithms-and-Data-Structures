namespace AaDS.String;

public static class EqualFrequency
{
    /// <summary>
    /// You are given a 0-indexed string word, consisting of lowercase English letters.
    /// You need to select one index and remove the letter at that index from word so that the frequency of every letter present in word is equal.
    /// Note:
    /// 1. The frequency of a letter x is the number of times it occurs in the string.
    /// 2. You must remove exactly one letter and cannot choose to do nothing.
    /// </summary>
    /// <param name="word"></param>
    /// <returns>true if it is possible to remove one letter so that the frequency of all letters in word are equal, and false otherwise.</returns>
    public static bool EqualFrequencyI(string word) {
        int[] frequency = new int[26];

        for (int i = 0; i < word.Length; i++) {
            frequency[word[i] - 'a']++;
        }

        for (int i = 0; i < 26; i++) {
            if (frequency[i] == 0) continue; 

            frequency[i]--;

            if (frequency.Where(f => f > 0).Distinct().Count() == 1) {
                return true;
            }

            frequency[i]++;
        }

        return false;
    }
}