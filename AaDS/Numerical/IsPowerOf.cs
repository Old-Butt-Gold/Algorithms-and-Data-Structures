namespace AaDS.Numerical;

public static class IsPowerOf
{
    /// <summary>
    /// Given an integer n.
    /// An integer n is a power of two, if there exists an integer x such that n == 2^x.
    /// </summary>
    /// <param name="n"></param>
    /// <returns>true if it is a power of two. Otherwise, return false</returns>
    public static bool IsPowerOfTwo(int n) => n > 0 && (n & (n - 1)) == 0;

    /// <summary>
    /// Given an integer n.
    /// </summary>
    /// <param name="n"></param>
    /// <returns>true if it is a power of three. Otherwise, return false</returns>
    public static bool IsPowerOfThree(int n) =>
        // 1162261467 — max power of three in range of int
        n > 0 && 1162261467 % n == 0;

    /// <summary>
    /// Given an integer n.
    /// </summary>
    /// <param name="n"></param>
    /// <returns>return true if it is a power of four. Otherwise, return false</returns>
    public static bool IsPowerOfFour(int n) =>
        // Mask to check odd bit positions: 0x55555555 (binary: 01010101010101010101010101010101)
        n > 0 && (n & (n - 1)) == 0 && (n & 0x55555555) != 0;
}