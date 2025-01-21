namespace AaDS.DynamicProgramming._1DP;

public static class LongestPalindromicSubstring
{
    /// <summary>
    /// Given a string s, return the longest palindromic substring in s.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string LongestPalindrome(string input)
    {
        int length = input.Length;

        // Таблица для хранения информации о том, является ли подстрока палиндромом
        var isPalindrome = new bool[length, length];

        // Диапазон индексов самой длинной палиндромной подстроки
        int[] longestRange = [0, 0];

        // Все одиночные символы — это палиндромы
        for (int i = 0; i < length; i++)
        {
            isPalindrome[i, i] = true;
        }

        // Проверяем подстроки длиной 2
        for (int i = 0; i < length - 1; i++)
        {
            if (input[i] == input[i + 1])
            {
                isPalindrome[i, i + 1] = true;
                longestRange = [i, i + 1];
            }
        }

        // Проверяем подстроки длиной от 3 и больше
        for (int currentLength = 3; currentLength <= length; currentLength++)
        {
            for (int start = 0; start <= length - currentLength; start++)
            {
                int end = start + currentLength - 1;

                // Подстрока является палиндромом, если:
                // 1. Крайние символы равны
                // 2. Вложенная подстрока также является палиндромом
                if (input[start] == input[end] && isPalindrome[start + 1, end - 1])
                {
                    isPalindrome[start, end] = true;
                    longestRange = [start, end];
                }
            }
        }

        // Извлекаем самую длинную палиндромную подстроку по вычисленным индексам
        int startIdx = longestRange[0];
        int endIdx = longestRange[1];
        return input.Substring(startIdx, endIdx - startIdx + 1);
    }

}