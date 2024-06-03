using System.Text;

namespace AaDS.String;

public static class RepeatedSubstringPattern
{
    /// <summary>
    /// Given a string s, check if it can be constructed by taking a substring of it and appending multiple copies of the substring together.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool RepeatedSubstringPatternI(string s)
    {
        StringBuilder sb = new(s);
        sb.Append(s);
        //remove first and last symbol
        sb.Remove(0, 1);
        sb.Length--;
        return sb.ToString().Contains(s);
    }
    
    public static bool RepeatedSubstringPatternII(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        int[] lps = KMP.BuildLPS(s);

        int len = lps[^1];

        return len > 0 && s.Length % (s.Length - len) == 0;
    }
}