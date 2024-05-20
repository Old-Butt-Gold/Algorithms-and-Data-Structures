using System.Text;

namespace AaDS.Bits;

public static class AddBinary
{
    public static string Add(string a, string b)
    {
        char carry = '0';
        int i = a.Length - 1;
        int j = b.Length - 1;
        StringBuilder sb = new();

        while (i > -1 || j > -1)
        {
            char tempA = i > -1 ? a[i] : '0';
            char tempB = j > -1 ? b[j] : '0';
            if (tempA == tempB)
            {
                sb.Append(carry);
                carry = tempA;
            }
            else
            {
                sb.Append(carry == '0' ? '1' : '0');
            }

            i--;
            j--;
        }

        if (carry == '1')
        {
            sb.Append('1');
        }

        int middle = sb.Length / 2;
        for (int index = 0; index < middle; index++)
        {
            (sb[index], sb[^(1 + index)]) = (sb[^(1 + index)], sb[index]);
        }

        return sb.ToString();
    }
}