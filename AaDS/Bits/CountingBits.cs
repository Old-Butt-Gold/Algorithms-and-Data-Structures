namespace AaDS.Bits;

public class CountingBits
{
    /// <summary>
    /// Given an integer n, return an array ans of length n + 1 such that for each i (0 <= i <= n), ans[i] is the number of 1's in the binary representation of i.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int[] CountBits(int n) {
        var ans = new int[n + 1];
        for (int i = 1; i <= n; i++) {
            ans[i] = ans[i >> 1] + (i & 1);
        }
        return ans;
    }
}