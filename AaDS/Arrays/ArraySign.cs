namespace AaDS.Arrays;

public static class ArraySign
{
    /// <summary>
    /// There is a function signFunc(x) that returns:
    /// 1 if x is positive.
    /// -1 if x is negative.
    /// 0 if x is equal to 0.
    /// You are given an integer array nums. Let product be the product of all values in the array nums.
    /// Return signFunc(product).
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int ArraySignI(int[] nums)
    {
        int neg = 0;

        foreach (var num in nums)
        {
            if (num == 0) 
                return 0;

            if (num < 0)
            {
                neg++;
            }
        }

        return neg % 2 == 0 ? 1 : -1;
    }
}