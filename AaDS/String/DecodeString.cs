using System.Text;

namespace AaDS.String;

public static class DecodeString
{
    /// <summary>
    /// https://leetcode.com/problems/decode-string/?envType=study-plan-v2&envId=leetcode-75
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string DecodeStringI(string s)
    {
        int repeatCount = 0;
        StringBuilder result = new StringBuilder(s.Length);
        var stack = new Stack<(int startIndex, int repeatCount)>();

        foreach (var ch in s)
        {
            if (ch == '[')
            {
                stack.Push((result.Length, repeatCount));
                repeatCount = 0;
            }
            else if (ch == ']')
            {
                var (startIndex, times) = stack.Pop();

                string segment = result.ToString(startIndex, result.Length - startIndex);
                for (int i = 1; i < times; i++)
                {
                    result.Append(segment);
                }
            }
            else if (char.IsDigit(ch))
            {
                repeatCount = repeatCount * 10 + (ch - '0');
            }
            else
            {
                result.Append(ch);
            }
        }

        return result.ToString();
    }

}