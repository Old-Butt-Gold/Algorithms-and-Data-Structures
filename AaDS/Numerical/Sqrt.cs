namespace AaDS.Numerical;

public static class Sqrt
{
    public static double MySqrt(int x) {
        if (x is 0 or 1) return x;

        //метод Ньютона

        double precision = 1e-6; // Точность
        double guess = x / 2.0; // Начальное предположение

        while (Math.Abs(guess * guess - x) > precision) {
            guess = 0.5 * (guess + x / guess); // Обновление предположения
        }

        return Math.Floor(guess);
    }
}