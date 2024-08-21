namespace AaDS.DynamicProgramming._1DP;

public class WordBreakDP
{
    /// <summary>
    /// Given a string s and a dictionary of strings wordDict, return true if s can be segmented into a space-separated sequence of one or more dictionary words.
    /// Note that the same word in the dictionary may be reused multiple times in the segmentation.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="wordDict"></param>
    /// <returns></returns>
    public static bool WordBreak(string s, IList<string> wordDict)
    {
        bool[] dp = new bool[s.Length + 1];
        dp[0] = true;
        int maxLen = wordDict.Max(x => x.Length);

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = i - 1; j >= Math.Max(i - maxLen - 1, 0); j--)
            {
                if (dp[j] && wordDict.Contains(s.Substring(j, i - j)))
                {
                    dp[i] = true;
                    break;
                }
            }
        }

        return dp[s.Length];
    }
}