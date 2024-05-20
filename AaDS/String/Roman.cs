using System.Text;

namespace AaDS.String;

public static class Roman
{
    public static int RomanToInt(string s)
    {
        Dictionary<char, int> dictionary = new()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };

        int startValue = dictionary[s[^1]];
        for (int i = s.Length - 2; i > -1; i--)
        {
            if (dictionary[s[i]] < dictionary[s[i + 1]])
            {
                startValue -= dictionary[s[i]];
            }
            else
            {
                startValue += dictionary[s[i]];
            }
        }

        return startValue;
    }

    public static string IntToRoman(int num)
    {
        int[] codes = [1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1];
        string[] romanCodes = ["M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"];
        StringBuilder sb = new();

        for (int i = 0; i < codes.Length; i++)
        {
            int digit = num / codes[i];

            while (digit-- > 0)
            {
                sb.Append(romanCodes[i]);
            }

            num %= codes[i];
        }
        
        return sb.ToString();
    }
}