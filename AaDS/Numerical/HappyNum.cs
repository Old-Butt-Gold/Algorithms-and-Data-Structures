namespace AaDS.Numerical;

public static class HappyNum
{
    ///<summary>
    ///Write an algorithm to determine if a number n is happy.
    /// A happy number is a number defined by the following process:
    /// Starting with any positive integer, replace the number by the sum of the squares of its digits.
    /// Repeat the process until the number equals 1 (where it will stay), or it loops endlessly in a cycle which does not include 1.
    /// Those numbers for which this process ends in 1 are happy.
    ///</summary>
    ///<param name="n"></param>
    ///<returns></returns>
    public static bool IsHappy(int n)
    {
        HashSet<int> values = [];
        while (n != 1)
        {
            values.Add(n);
            n = SquareSum(n);
            if (values.Contains(n)) return false;
        }

        return true;

        int SquareSum(int number)
        {
            int fast = 0;
            while (number > 0)
            {
                int digit = number % 10;
                fast += digit * digit;
                number /= 10;
            }

            return fast;
        }
    }
}