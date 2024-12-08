namespace AaDS.Sortings;

public static class StringCompression
{
    /// <summary>
    /// https://leetcode.com/problems/string-compression/?envType=study-plan-v2&envId=leetcode-75
    /// </summary>
    /// <param name="chars"></param>
    /// <returns></returns>
    public static int Compress(char[] chars)
    {
        int readIndex = 0;  
        int writeIndex = 0; 

        while (readIndex < chars.Length)
        {
            char currentChar = chars[readIndex];
            int count = 0;

            while (readIndex < chars.Length && chars[readIndex] == currentChar)
            {
                readIndex++;
                count++;
            }

            chars[writeIndex++] = currentChar;

            if (count > 1)
            {
                foreach (char digit in count.ToString())
                {
                    chars[writeIndex++] = digit;
                }
            }
        }

        return writeIndex;
    }
}