namespace AaDS.Numerical;

public static class IsPalindrome
{
    public static bool IsPalindromeCheck(int x)
    {
        if (x is 0) return true;
        if (x < 0 || x % 10 == 0) return false;

        int half = 0;
        while (half < x)
        {
            half = half * 10 + x % 10;
            x /= 10;
        }

        return x == half || x == half / 10;
    }
}