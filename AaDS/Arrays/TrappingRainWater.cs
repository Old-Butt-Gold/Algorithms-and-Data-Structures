namespace AaDS.Arrays;

public class TrappingRainWater
{
    public static int Trap(int[] height)
    {
        if (height.Length is 0) return 0;

        int result = 0;

        int left = 0;
        int right = height.Length - 1;
        int maxLeft = height[left];
        int maxRight = height[right];

        while (left < right)
        {
            if (maxLeft <= maxRight)
            {
                left++;
                result += Math.Max(0, maxLeft - height[left]);
                maxLeft = Math.Max(maxLeft, height[left]);
            }
            else
            {
                right--;
                result += Math.Max(0, maxRight - height[right]);
                maxRight = Math.Max(maxRight, height[right]);
            }
        }

        return result;
    }
}