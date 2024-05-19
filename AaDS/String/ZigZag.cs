using System.Text;

namespace AaDS.String;

public class ZigZag
{
    public string Convert(string s, int numRows)
    {
        if(numRows == 1 || s.Length < 2)
        {
            return s;
        }

        StringBuilder[] solution = new StringBuilder[numRows];
        for (int j = 0; j < solution.Length; j++)
        {
            solution[j] = new StringBuilder();
        }

        int i = 0, direction = 1;
        foreach(char c in s)
        {
            solution[i].Append(c);

            i += direction;

            if (i == numRows - 1 || i == 0) direction *= -1;
        }

        StringBuilder result = new();
        foreach (var item in solution)
        {
            result.Append(item);
        }

        return result.ToString();
    }
}