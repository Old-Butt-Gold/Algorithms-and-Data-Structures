namespace AaDS.Bits;

public static class NeighboringBitwiseXor
{
    /// <summary>
    /// https://leetcode.com/problems/neighboring-bitwise-xor/description/?envType=daily-question
    /// </summary>
    /// <param name="derived"></param>
    /// <returns></returns>
    public static bool DoesValidArrayExist(int[] derived)
    {
        int xor = 0;
        
        //derived[0] = original[0] xor original[1]
        //derived[1] = original[1] xor original[0]
        // so it comes that original[0] xor original[1] xor original[1] xor original[0] should give 0

        foreach (var element in derived)
            xor ^= element;

        return xor == 0;
    }
}