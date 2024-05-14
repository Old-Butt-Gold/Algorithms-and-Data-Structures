namespace AaDS.String;

static class KMP
{
    /// <summary>
    ///     Returns the start index of the first appearance
    ///     of the pattern in the input string.
    ///     Returns -1 if no match.
    /// </summary>
    static int Search(string input, string pattern)
    {
        var isMatchingInProgress = false;
        var matchIndex = new int[pattern.Length];
        var patternIndex = 0;

        for (var i = 1; i < pattern.Length; i++)
        {
            if (pattern[i] != pattern[patternIndex])
            {
                if (isMatchingInProgress) i--;
                isMatchingInProgress = false;
                patternIndex = matchIndex[patternIndex == 0 ? 0 : patternIndex - 1];
            }
            else
            {
                isMatchingInProgress = true;
                matchIndex[i] = patternIndex + 1;
                patternIndex++;
            }
        }

        isMatchingInProgress = false;
        patternIndex = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == pattern[patternIndex])
            {
                isMatchingInProgress = true;
                patternIndex++;

                if (patternIndex == pattern.Length) return i - pattern.Length + 1;
            }
            else
            {
                if (isMatchingInProgress) i--;
                isMatchingInProgress = false;
                if (patternIndex != 0) patternIndex = matchIndex[patternIndex - 1];
            }
        }

        return -1;
    }
}