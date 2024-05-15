namespace AaDS.Numerical;

class SimplePrimeTester
{
    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;

        if (number <= 3) return true;

        if (number % 2 == 0 || number % 3 == 0) return false;

        var sqrt = Math.Sqrt(number);
        for (var i = 5; i <= sqrt; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
                return false;
        }
        return true;
    }
}