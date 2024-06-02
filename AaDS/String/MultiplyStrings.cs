using System.Text;

namespace AaDS.String;

public static class MultiplyStrings
{
    /// <summary>
    /// Given two non-negative integers num1 and num2 represented as strings, return the product of num1 and num2, also represented as a string.
    /// Note: You must not use any built-in BigInteger library or convert the inputs to integer directly.
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    public static string Multiply(string num1, string num2)
    {
        int[] digits = new int[num1.Length + num2.Length];

        for (int i = num1.Length - 1; i > -1; i--)
        {
            for (int j = num2.Length - 1; j > -1; j--)
            {
                int digit1 = num1[i] - '0';
                int digit2 = num2[j] - '0';
                int sum = digits[i + j + 1] + digit1 * digit2;

                digits[i + j + 1] = sum % 10;
                digits[i + j] += sum / 10;
            }
        }

        StringBuilder stringBuilder = new();

        int index = 0;
        
        //for skipping leading zeroes
        while (index < digits.Length && digits[index] == 0)
        {
            index++;
        }

        if (index == digits.Length)
        {
            return "0";
        }

        for (int i = index; i < digits.Length; i++)
        {
            stringBuilder.Append(digits[i]);
        }

        return stringBuilder.ToString();
    } 
}