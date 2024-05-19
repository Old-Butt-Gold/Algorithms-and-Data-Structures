namespace AaDS.Bits;

public static class HammingWeight
{
    /// <summary>
    /// returns the number of set bits (1)
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int CalculateHammingWeight(int n)
    {
        int count = 0;
        while (n > 0)
        {
            count++;
            n &= (n - 1);
        }

        return count;
    }
}