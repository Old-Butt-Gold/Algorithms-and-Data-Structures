namespace AaDS.String;

static class KMP
{
    public static int KMPSearch(string haystack, string needle, int[] lps)
    {
        if (string.IsNullOrEmpty(needle)) 
            return 0; 
        
        int needleIndex = 0; // Указатель на текущий символ в needle
        for (int haystackIndex = 0; haystackIndex < haystack.Length; haystackIndex++)
        {
            // Если текущие символы не совпадают
            while (needleIndex > 0 && needle[needleIndex] != haystack[haystackIndex])
            {
                needleIndex = lps[needleIndex - 1]; // Используем LPS для отката
            }

            // Если символы совпадают, продвигаемся дальше
            if (needle[needleIndex] == haystack[haystackIndex])
            {
                needleIndex++;
            }

            // Если дошли до конца needle, значит, найдено совпадение
            if (needleIndex == needle.Length)
            {
                return haystackIndex - needleIndex + 1; // Индекс начала совпадения
            }
        }

        return -1;
    }
    
    /// <summary>
    ///     Returns the start index of the first appearance
    ///     of the pattern `needle` in the input string `haystack`.
    ///     Returns -1 if no match.
    /// </summary>
    public static int KMPSearch(string haystack, string needle)
    {
        if (string.IsNullOrEmpty(needle)) 
            return 0; 
        
        var lps = BuildLPS(needle);

        return KMPSearch(haystack, needle, lps);
    }

    /// <summary>
    /// builds array LPS (Longest Prefix Suffix) for `pattern` string
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static int[] BuildLPS(string pattern)
    {
        int[] lps = new int[pattern.Length];
        int length = 0; // Длина текущего совпадающего префикса-суффикса

        // Проходим по pattern, начиная со второго символа
        for (int i = 1; i < pattern.Length; i++)
        {
            // Если символы не совпадают, пытаемся сократить длину совпадения
            while (length > 0 && pattern[i] != pattern[length])
            {
                length = lps[length - 1];
            }

            // Если символы совпадают, увеличиваем длину совпадения
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
            }
        }

        return lps;
    }

}