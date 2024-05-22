namespace AaDS.DynamicProgramming._1DP;

public static class ClimbingStairs
{
    /// <summary>
    /// You are climbing a staircase. It takes n steps to reach the top.
    /// Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int ClimbStairs(int n)
    {
        if (n < 2) return 1;

        int[] list = [1, 1]; //for 0 and 1 staircase;
        for (int i = 2; i <= n; i++)
        {
            int temp = list[0] + list[1];
            list[0] = list[1];
            list[1] = temp;
        }

        return list[1];
    }
}