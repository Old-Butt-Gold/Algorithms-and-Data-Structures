namespace AaDS.Numerical;

public static class Factorial
{
    /// <summary>
    /// Given an integer n, return the number of trailing zeroes in n! (factorial).
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int TrailingZeroes(int n)
    {
        int counter = 0;
        while (n > 0)
        {
            counter += n / 5;
            n /= 5;
        }

        return counter;
    }
}