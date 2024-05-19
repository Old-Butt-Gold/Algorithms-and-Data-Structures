namespace AaDS.Bits;

public static class ReverseBits
{
    public static uint Reverse(uint n)
    {
        uint reversed = 0;

        for (int i = 0; i < 32; i++)
        {
            reversed <<= 1;
            reversed |= n & 1;

            n >>= 1;
        }

        return reversed;
    }

}