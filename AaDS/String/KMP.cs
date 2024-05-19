namespace AaDS.String;

static class KMP
{
    /// <summary>
    ///     Returns the start index of the first appearance
    ///     of the pattern in the input string.
    ///     Returns -1 if no match.
    /// </summary>
    public static int KMPSearch(string haystack, string needle)
    {
        if (string.IsNullOrEmpty(needle)) return 0; // если needle пуст, вернуть 0
        int[] lps = new int[needle.Length];

        // Препроцессинг
        int pre = 0;
        for (int i = 1; i < needle.Length; i++)
        {
            while (pre > 0 && needle[i] != needle[pre])
            {
                pre = lps[pre - 1];
            }
            if (needle[pre] == needle[i])
            {
                pre++;
                lps[i] = pre;
            }
        }

        // Основной алгоритм
        int n = 0; // индекс для needle
        for (int h = 0; h < haystack.Length; h++)
        {
            while (n > 0 && needle[n] != haystack[h])
            {
                n = lps[n - 1];
            }
            if (needle[n] == haystack[h])
            {
                n++;
            }
            if (n == needle.Length)
            {
                return h - n + 1;
            }
        }

        return -1;
    }

}