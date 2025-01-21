namespace AaDS.BackTracking;

public static class LetterCombination
{
    private static readonly Dictionary<char, string> _dictionary = new()
    {
        { '2', "abc" },
        { '3', "def" },
        { '4', "ghi" },
        { '5', "jkl" },
        { '6', "mno" },
        { '7', "pqrs" },
        { '8', "tuv" },
        { '9', "wxyz" },
    };
    
    /// <summary>
    /// https://leetcode.com/problems/letter-combinations-of-a-phone-number/description/?envType=study-plan-v2
    /// </summary>
    /// <param name="digits"></param>
    /// <returns></returns>
    public static IList<string> LetterCombinations(string digits)
    {
        if (string.IsNullOrEmpty(digits)) return [];

        List<string> result = [];
        var combination = new char[digits.Length];
        
        Backtrack(0);
        return result;
        
        void Backtrack(int index)
        {
            if (index == digits.Length)
            {
                result.Add(new(combination));
                return;
            }

            foreach (var ch in _dictionary[digits[index]])
            {
                combination[index] = ch;
                Backtrack(index + 1);
            }
        }
    }
}