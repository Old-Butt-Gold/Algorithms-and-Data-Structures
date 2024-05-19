namespace AaDS.Bits;

public static class RangeAnd
{
    public static int RangeBitwiseAnd(int left, int right)
    {
        int counter = 0;
        while (left != right)
        {
            counter++;
            left >>= 1;
            right >>= 1;
        }

        return left << counter;
    }
}