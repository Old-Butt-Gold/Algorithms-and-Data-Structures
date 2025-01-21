namespace AaDS.String;

public static class WordSubsets
{
    /// <summary>
    /// https://leetcode.com/problems/word-subsets/description/?envType=daily-question
    /// </summary>
    /// <param name="words1"></param>
    /// <param name="words2"></param>
    /// <returns></returns>
    public static IList<string> WordSubsetsI(string[] words1, string[] words2)
    {
        var bMax = new int[26];
        foreach (var b in words2)
        {
            var bCount = Count(b);
            for (int i = 0; i < 26; i++)
            {
                bMax[i] = Math.Max(bMax[i], bCount[i]);
            }
        }

        List<string> result = [];
        foreach (var a in words1)
        {
            var aCount = Count(a);
            if (IsUniversal(aCount, bMax))
            {
                result.Add(a);
            }
        }

        return result;

        int[] Count(string s)
        {
            var count = new int[26];
            foreach (var c in s)
            {
                count[c - 'a']++;
            }

            return count;
        }

        bool IsUniversal(int[] aCount, int[] bMax)
        {
            for (int i = 0; i < 26; i++)
            {
                if (aCount[i] < bMax[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}