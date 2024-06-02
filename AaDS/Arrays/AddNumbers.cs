namespace AaDS.Arrays;

public static class AddNumbers
{
    public static int[] Add(int[] num1, int[] num2)
    {
        int maxLength = Math.Max(num1.Length, num2.Length);
        int[] result = new int[maxLength + 1]; // +1 для возможного переноса из последнего разряда
        int i = num1.Length - 1;
        int j = num2.Length - 1;
        int carry = 0;
    
        while (i > -1 || j > -1 || carry != 0)
        {
            int digit1 = i > -1 ? num1[i] : 0;
            int digit2 = j > -1 ? num2[j] : 0;
        
            int sum = digit1 + digit2 + carry;
        
            result[maxLength] = sum % 10;
            carry = sum / 10;
        
            // Двигаем указатели влево
            i--;
            j--;
            maxLength--;
        }
    
        int index = 0;
        while (index < result.Length && result[index] == 0)
        {
            index++;
        }
        
        if (index == result.Length)
        {
            return [0];
        }
        
        var finalResult = new int[result.Length - index];
        Array.Copy(result, index, finalResult, 0, finalResult.Length);
        return finalResult;
    }

    /// <summary>
    /// The array-form of an integer num is an array representing its digits in left to right order.
    /// For example, for num = 1321, the array form is [1,3,2,1].
    /// Given num, the array-form of an integer, and an integer k, return the array-form of the integer num + k.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static IList<int> AddToArrayForm(int[] num, int k)
    {
        List<int> result = [];
        int carry = 0;
        int i = num.Length - 1;
        while (k > 0 || carry > 0 || i > -1)
        {
            int digit = i > -1 ? num[i] : 0;

            int sum = digit + k % 10 + carry;
            
            k /= 10;
            carry = sum / 10;
            result.Add(sum % 10);
            i--;
        }

        result.Reverse();

        return result;
    }

}