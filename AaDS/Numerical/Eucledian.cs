using System.Numerics;

namespace AaDS.Numerical;

static class Eucledian
{
    public static int FindGcd(int a, int b) => b == 0 ? a : FindGcd(b, a % b);

    public static int FindLcm(int a, int b) => a * b / FindGcd(a, b);

    //Вычисляет количество взаимно простых чисел до n 
    public static BigInteger EulerPhi(BigInteger n)
    {
        BigInteger result = n; // Изначально присваиваем результату значение n

        // Перебираем все простые числа, которые делят n
        for (BigInteger p = 2; p * p <= n; ++p)
        {
            if (n % p == 0)
            {
                // Если p делит n, уменьшаем result на result / p и на p - 1
                while (n % p == 0)
                {
                    n /= p;
                }
                result -= result / p;
            }
        }

        // Если n осталось простым, уменьшаем result на result / n
        if (n > 1)
        {
            result -= result / n;
        }

        return result;
    }
    
    public static (int gcd, int x, int y) ExtendedEuclidean(int a, int b)
    {
        // Инициализация начальных значений
        int x0 = 1, y0 = 0, x1 = 0, y1 = 1;
        int d0 = a, d1 = b; //поменять a и b местами для алгоритма по таблице

        while (d1 != 0)
        {
            // Вычисление остатка и частного
            int q = d0 / d1;
            int d2 = d0 % d1;
            int x2 = x0 - q * x1;
            int y2 = y0 - q * y1;

            // Обновление значений
            d0 = d1;
            d1 = d2;
            x0 = x1;
            x1 = x2;
            y0 = y1;
            y1 = y2;
        }

        if (y0 < 0) //Для RSA, не для рабина
        {
            y0 += a;
        }

        // Возвращаем НОД и коэффициенты x и y
        return (d0, x0, y0);
    }


}