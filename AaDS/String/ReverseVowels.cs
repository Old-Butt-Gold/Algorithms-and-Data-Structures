using System.Text;

namespace AaDS.String;

public class ReverseVowels
{
    public static string ReverseVowelsStr(string s)
    {
        StringBuilder sb = new(s);

        string letters = "aeiouAEIOU";

        int left = 0;
        int right = sb.Length - 1;

        while (left < right) {
            
            while (left < s.Length && !letters.Contains(s[left])) {
                left++;
            }

            while (right > -1 && !letters.Contains(s[right])) {
                right--;
            }

            if (left < right) {
                (sb[left], sb[right]) = (sb[right], sb[left]);
                left++;
                right--;
            }
        }
        
        return sb.ToString();
    }
}