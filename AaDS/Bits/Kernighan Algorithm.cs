namespace AaDS.Bits;

public static class KernighanAlgorithm
{
    /// <summary>
    /// Count bits in number in O(0) space complexity and O(n) time complexity 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static int BitCount(int x)
    {
        int count = 0;
        while (x > 0)
        {
            count++;
            x &= (x - 1);
        }

        return count;
    }
}