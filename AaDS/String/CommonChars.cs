namespace AaDS.String;

public static class CommonChars
{
    /// <summary>
    /// Given a string array words, return an array of all characters that show up in all strings within the words (including duplicates). You may return the answer in any order.
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public static IList<string> CommonCharsI(string[] words)
    {
        List<string> result = [];
        int[] dict = new int[26];
        for (int i = 0; i < words[0].Length; i++)
        {
            dict[words[0][i] - 'a']++;
        }

        for (int i = 1; i < words.Length; i++)
        {
            int[] curr = new int[26];

            for (int j = 0; j < words[i].Length; j++)
            {
                curr[words[i][j] - 'a']++;
            }


            for (int j = 0; j < 26; j++)
            {
                if (curr[j] < dict[j])
                {
                    dict[j] = curr[j];
                }
            }
        }

        for (int i = 0; i < 26; i++)
        {
            while (dict[i] > 0)
            {
                result.Add(((char)(i + 'a')).ToString());
                dict[i]--;
            }
        }

        return result;
    }
}