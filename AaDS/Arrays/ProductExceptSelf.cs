namespace AaDS.Arrays;

public static class ProductExceptSelf
{
    /// <summary>
    /// Given an integer array nums, return an array answer such that answer[i] is equal to the product of all the elements of nums except nums[i].
    /// The product of any prefix or suffix of nums is guaranteed to fit in a 32-bit integer.
    /// You must write an algorithm that runs in O(n) time and without using the division operation.
    /// HINT: Prefix Sum 
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int[] ProductExcept(int[] nums)
    {
        var result = new int[nums.Length];

        int leftProduct = 1;
        for (int i = 0; i < nums.Length; i++)
        {
            result[i] = leftProduct;
            leftProduct *= nums[i];
        }

        int rightProduct = 1;
        for (int i = nums.Length - 1; i > -1; i--)
        {
            result[i] *= rightProduct;
            rightProduct *= nums[i];
        }

        return result;
    }
}