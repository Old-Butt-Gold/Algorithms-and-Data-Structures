namespace AaDS.DynamicProgramming._1DP;

public static class Tribonacci
{
    public static int TribonacciNumber(int n)
    {
        int[] nums = [0, 1, 1];

        if (n < 3)
        {
            return nums[n];
        }

        for (int i = 3; i <= n; i++)
        {
            int temp = nums[0] + nums[1] + nums[2];
            nums[0] = nums[1];
            nums[1] = nums[2];
            nums[2] = temp;
        }

        return nums[2];
    }
}