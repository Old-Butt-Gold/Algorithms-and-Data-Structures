using System.Text;

namespace AaDS.String;

static class ManachersPalindrome
{
    public static string FindLongestPalindrome(string input)
    {
        if (input.Length <= 1) throw new ArgumentException("Invalid input");
        if (input.Contains("$")) throw new Exception("Input contains sentinel character $.");

        var charArray = input.ToCharArray();
        var modifiedInput = new StringBuilder();

        foreach (var character in charArray)
        {
            modifiedInput.Append("$");
            modifiedInput.Append(character);
        }

        modifiedInput.Append("$");

        var result = FindLongestPalindromeHelper(modifiedInput.ToString());

        return result.Replace("$", "");
    }

    static string FindLongestPalindromeHelper(string input)
    {
        var palindromeLengths = new int[input.Length];

        int left = -1, right = 1;
        var length = 1;

        var currentIndex = 0;
        while (currentIndex < input.Length)
        {
            while (left >= 0 && right < input.Length)
            {
                if (input[left] == input[right])
                {
                    left--;
                    right++;
                    length += 2;
                }
                else
                {
                    break;
                }
            }

            var continueExploring = false;

            palindromeLengths[currentIndex] = length;

            if (right > currentIndex + 2)
            {
                var leftIndex = currentIndex - 1;
                var rightIndex = currentIndex + 1;

                while (rightIndex < right)
                {
                    var mirrorLength = palindromeLengths[leftIndex];

                    if (leftIndex - mirrorLength / 2 < left + 1)
                    {
                        palindromeLengths[rightIndex] = 2 * (leftIndex - (left + 1)) + 1;
                        rightIndex++;
                        leftIndex--;
                    }
                    else if (leftIndex - mirrorLength / 2 > left + 1 && rightIndex + mirrorLength / 2 < right - 1)
                    {
                        palindromeLengths[rightIndex] = palindromeLengths[leftIndex];
                        rightIndex++;
                        leftIndex--;
                    }
                    else
                    {
                        length = palindromeLengths[leftIndex];
                        currentIndex = rightIndex;
                        left = currentIndex - length / 2 - 1;
                        right = currentIndex + length / 2 + 1;

                        continueExploring = true;
                        break;
                    }
                }

                currentIndex = rightIndex;
            }

            if (continueExploring) continue;

            left = currentIndex;
            right = currentIndex + 2;
            length = 1;

            currentIndex++;
        }

        var maxPalindromeLength = FindMax(palindromeLengths);
        var maxPalindromeCenterIndex = Array.IndexOf(palindromeLengths, maxPalindromeLength);

        var start = maxPalindromeCenterIndex - maxPalindromeLength / 2;
        var end = start + maxPalindromeLength - 1;

        return input.Substring(start, maxPalindromeLength);
    }

    static int FindMax(int[] palindromeLengths) => palindromeLengths.Concat(new[] { int.MinValue }).Max();
}