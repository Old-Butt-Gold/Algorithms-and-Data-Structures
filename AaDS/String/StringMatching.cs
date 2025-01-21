namespace AaDS.String;

public static class StringMatching
{
    /// <summary>
    /// Given an array of string words
    /// </summary>
    /// <param name="words"></param>
    /// <returns>all strings in words that is a substring of another word. You can return the answer in any order.</returns>
    public static IList<string> StringMatchingI(string[] words) {
        List<string> matchingWords = [];

        for (int currentWordIndex = 0; currentWordIndex < words.Length; currentWordIndex++)
        {
            var lps = KMP.BuildLPS(words[currentWordIndex]);

            if (words.Where((t, otherWordIndex) => currentWordIndex != otherWordIndex)
                .Any(t => KMP.KMPSearch(t, words[currentWordIndex], lps) != -1))
            {
                matchingWords.Add(words[currentWordIndex]);
            }
        }

        return matchingWords;
    }

    //XD one-line solution
    public static IList<string> StringMatchingII(string[] words) 
        => words.Where(x => words.Where(w => w != x).
            Any(w => w.Contains(x))).ToList();
}