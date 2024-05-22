namespace AaDS.DynamicProgramming._1DP;

public static class Fibonacci
{
    public static int Fib(int n)
    {
        if (n < 2) return n;

        int[] list = [0, 1];
        for (int i = 2; i <= n; i++)
        {
            int temp = list[0] + list[1];
            list[0] = list[1];
            list[1] = temp;
        }

        return list[1];
    }
}
