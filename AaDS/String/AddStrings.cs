using System.Text;

namespace AaDS.String;

public static class AddStrings
{
    /// <summary>
    /// Given two non-negative integers, num1 and num2 represented as string, return the sum of num1 and num2 as a string.
    /// You must solve the problem without using any built-in library for handling large integers (such as BigInteger). You must also not convert the inputs to integers directly.
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    public static string Add(string num1, string num2)
    {
        StringBuilder sb = new();
        int carry = 0;
        int i = num1.Length - 1;
        int j = num2.Length - 1;

        while (i > -1 || j > -1 || carry != 0)
        {
            int digit1 = i > -1 ? num1[i] - '0' : 0;
            int digit2 = j > -1 ? num2[j] - '0' : 0;
            int sum = digit1 + digit2 + carry;
            
            carry = sum / 10;
            sb.Append(sum % 10);

            i--;
            j--;
        }

        for (int index = 0; index < sb.Length / 2; index++)
        {
            (sb[index], sb[sb.Length - 1 - index]) = (sb[sb.Length - 1 - index], sb[index]);
        }

        return sb.ToString();
    }
}