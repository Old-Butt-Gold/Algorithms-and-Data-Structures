namespace AaDS.Arrays;

public static class MultiplyNumbers
{
    public static int[] Multiply(int[] num1, int[] num2)
    {
        int[] digits = new int[num1.Length + num2.Length];

        for (int i = num1.Length - 1; i > -1; i--)
        {
            for (int j = num2.Length - 1; j > -1; j--)
            {
                int product = num1[i] * num2[j];
                int sum = product + digits[i + j + 1];

                digits[i + j + 1] = sum % 10;
                digits[i + j] += sum / 10;
            }
        }
        
        // Пропускаем ведущие нули
        int index = 0;
        while (index < digits.Length && digits[index] == 0)
        {
            index++;
        }

        if (index == digits.Length)
        {
            return [0];
        }
        
        int[] result = new int[digits.Length - index];
        Array.Copy(digits, index, result, 0, result.Length);
        return result;
    } 
}