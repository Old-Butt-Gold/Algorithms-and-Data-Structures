namespace AaDS.Bits;

public static class MinimizeXOR
{
    /// <summary>
    /// https://leetcode.com/problems/minimize-xor/description/?envType=daily-question
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    public static int MinimizeXor(int num1, int num2)
    {
        var m = KernighanAlgorithm.BitCount(num1);
        var n = KernighanAlgorithm.BitCount(num2);

        // If num2 has more single bits, add the missing bits to num1.
        for (var i = 0; i < n - m; i++)
            num1 |= num1 + 1; // Set the next zero bit
        // If num1 has more single bits, discard the extra bits
        for (var i = 0; i < m - n; i++)
            num1 &= num1 - 1; // Reset the low bit set
        return num1;
    }
}