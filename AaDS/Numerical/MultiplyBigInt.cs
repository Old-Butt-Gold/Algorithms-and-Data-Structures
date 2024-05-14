namespace AaDS.Numerical;

static class MultiplyBigInt
{

    public static double PowerMod(double num, int power, int mod)
    {
        if (mod == 1) 
            return 0;

        if (power == 0)
            return 1;
        
        if (num == 0)
            return 0;

        double result = 1;
        double current = num;
        int exponent = Math.Abs(power);

        while (exponent > 0)
        {
            if (exponent % 2 == 1)
                result = (result * current) % mod;

            current = (current * current) % mod;
            exponent /= 2;
        }

        return power < 0 ? ModInverse(result, mod) : result;
        
        static double ModInverse(double a, int m)
        {
            double m0 = m;
            double y = 0, x = 1;

            while (a > 1)
            {
                double q = Math.Floor(a / m);
                double t = m;
                m = (int)(a % m);
                a = t;
                t = y;
                y = x - q * y;
                x = t;
            }

            if (x < 0)
                x += m0;

            return x;
        }
    }
    
    public static double Power(double x, int n)
    {
        if (n == 0)
            return 1;

        double result = 1;
        double current = x;
        int exponent = Math.Abs(n);

        while (exponent > 0)
        {
            if (exponent % 2 == 1)
                result *= current;

            current *= current;
            exponent /= 2;
        }

        return n < 0 ? 1 / result : result;
    }
    
    public static int[] BigPower(int number, int iterations)
    {
        int[] result = new int[GetNumberOfDigitsInPower(number, iterations)];
        result[^1] = 1;
        int index = -1;
        for (int i = 0; i < iterations; i++)
            MultiplyByTwo(result, number, ref index);
        return result;
        
        static void MultiplyByTwo(int[] number, int multiplyNumber, ref int index)
        {
            int carry = 0;
            int i;
            for (i = number.Length - 1; i > index; i--)
            {
                int product = number[i] * multiplyNumber + carry;
                number[i] = product % 10;
                carry = product / 10;
            }

            index = i;
        }

        static int GetNumberOfDigitsInPower(int baseNumber, int exponent) => (int)Math.Floor(exponent * Math.Log10(baseNumber)) + 1;

    }

}