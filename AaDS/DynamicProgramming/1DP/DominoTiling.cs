namespace AaDS.DynamicProgramming._1DP;

public static class DominoTiling
{
    /// <summary>
    /// https://leetcode.com/problems/domino-and-tromino-tiling/description/?envType=study-plan-v2
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int NumTilings(int n)
    {
        var modVal = (int)Math.Pow(10, 9) + 7;

        if (n < 3) return n;
        if (n == 3) return 5;

        var ways = new int[n];
        ways[0] = 1;
        ways[1] = 2;
        ways[2] = 5;

        for (int i = 3; i < ways.Length; i++)
        {
            ways[i] = (2 * ways[i - 1] % modVal + ways[i - 3] % modVal) % modVal;
        }

        return ways[^1];
    }
}