namespace AaDS.String;

public static class LongestPalindromicSubstring
{
    /// <summary>
    /// Given a string s, return the longest palindromic substring in s.
    /// This algorithm also known as Manacher's Palindrome.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        // Transform the string s into transformedString with boundaries and separators
        char[] transformedString = CreateTransformedString();
        int[] palindromeRadii = new int[transformedString.Length];
        
        int center = 0, rightBoundary = 0;

        for (int i = 1; i < transformedString.Length - 1; i++)
        {
            int mirror = 2 * center - i;
            if (i < rightBoundary)
            {
                palindromeRadii[i] = Math.Min(rightBoundary - i, palindromeRadii[mirror]);
            }

            // Attempt to expand palindrome centered at i
            while (transformedString[i + 1 + palindromeRadii[i]] == transformedString[i - 1 - palindromeRadii[i]])
            {
                palindromeRadii[i]++;
            }

            // If palindrome centered at i expands past rightBoundary, adjust center and rightBoundary
            if (i + palindromeRadii[i] > rightBoundary)
            {
                center = i;
                rightBoundary = i + palindromeRadii[i];
            }
        }

        // Find the maximum element in palindromeRadii
        int maxLength = 0;
        int centerIndex = 0;
        for (int i = 1; i < palindromeRadii.Length - 1; i++)
        {
            if (palindromeRadii[i] > maxLength)
            {
                maxLength = palindromeRadii[i];
                centerIndex = i;
            }
        }

        return s.Substring((centerIndex - maxLength) / 2, maxLength);

        char[] CreateTransformedString()
        {
            char[] transformed = new char[s.Length * 2 + 3];
            transformed[0] = '^';
            transformed[^1] = '$';
            for (int i = 0; i < s.Length; i++)
            {
                transformed[2 * i + 1] = '#';
                transformed[2 * i + 2] = s[i];
            }
            transformed[^2] = '#';
            return transformed;
        }
    }
}