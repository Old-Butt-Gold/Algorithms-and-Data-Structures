namespace AaDS.Numerical;

public class Reverse
{
    public int ReverseNumber(int x) {
        long num = 0;
        while (x != 0) {
            int digit = x % 10;
            num = num * 10 + digit;
            x /= 10;
        }

        return num is > int.MaxValue or < int.MinValue ? 0 : (int)num;
    }
}