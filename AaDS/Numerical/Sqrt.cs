namespace AaDS.Numerical;

public static class Sqrt
{
    public static double MySqrt(double x)
    {
        if (x is 0 or 1) return x;

        //метод Ньютона
        double precision = 1e-6; // Точность
        double guess = x / 2.0; // Начальное предположение

        while (Math.Abs(guess * guess - x) > precision)
        {
            guess = 0.5 * (guess + x / guess); // Обновление предположения
        }

        return Math.Floor(guess);
    }

    /// <summary>
    /// https://leetcode.com/problems/sqrtx/description/
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static int MySqrt(int x)
    {
        int left = 1;
        int right = x;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int sqrt = x / mid;

            if (sqrt == mid)
                return mid;
            
            if (sqrt < mid)
                right = mid - 1;
            else
                left = mid + 1;
        }

        return right;
    }
}