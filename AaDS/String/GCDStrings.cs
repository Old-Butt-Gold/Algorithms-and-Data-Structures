namespace AaDS.String;

public class GCDStrings
{
    /// <summary>
    /// https://leetcode.com/problems/greatest-common-divisor-of-strings/description/?envType=study-plan-v2&envId=leetcode-75
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <returns></returns>
    public string GcdOfStrings(string str1, string str2)
    {
        bool equality = (str1 + str2).Equals(str2 + str1);

        if (equality)
        {
            int gcdLength = GCD(str1.Length, str2.Length);
            return str1[..gcdLength];
        }

        return string.Empty;
        
        int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);
    }
}