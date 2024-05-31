using System.Text;

namespace AaDS.String;

public static class TextJustification
{
    /// <summary>
    /// https://leetcode.com/problems/text-justification/description/?envType=study-plan-v2&envId=top-interview-150
    /// </summary>
    /// <param name="words"></param>
    /// <param name="maxWidth"></param>
    /// <returns></returns>
    public static IList<string> FullJustify(string[] words, int maxWidth)
    {
        List<string> result = [];
        int startIndex = 0;

        while (startIndex < words.Length)
        {
            int endIndex = startIndex;
            int lineLength = 0;

            while (endIndex < words.Length && lineLength + words[endIndex].Length + (endIndex - startIndex) <= maxWidth)
            {
                lineLength += words[endIndex].Length;
                endIndex++;
            }

            int spaces = maxWidth - lineLength;
            int gaps = endIndex - startIndex - 1;

            StringBuilder lineBuilder = new();
            
            // If it's the last line or only one word in the line, left-justify
            if (endIndex == words.Length || gaps == 0)
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    lineBuilder.Append(words[i]);
                    lineBuilder.Append(' ');
                }

                lineBuilder.Length--; //remove the last space for word
                lineBuilder.Append(' ', maxWidth - lineBuilder.Length);
            }
            else
            {
                int spacesPerGap = spaces / gaps;
                int extraSpaces = spaces % gaps;

                for (int i = startIndex; i < endIndex; i++)
                {
                    lineBuilder.Append(words[i]);
                    
                    if (i < endIndex - 1)
                    {
                        int spacesToAdd = spacesPerGap + (i - startIndex < extraSpaces ? 1 : 0);
                        lineBuilder.Append(' ', spacesToAdd);
                    }
                }
            }

            result.Add(lineBuilder.ToString());
            startIndex = endIndex;
        }

        return result;
    }
}