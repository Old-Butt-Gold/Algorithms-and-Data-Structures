namespace AaDS.Arrays;

public static class ContainerWithMostWater
{
    /// <summary>
    /// You are given an integer array height of length n. There are n vertical lines drawn such that the two endpoints of the ith line are (i, 0) and (i, height[i]).
    /// Find two lines that together with the x-axis form a container, such that the container contains the most water.
    /// Return the maximum amount of water a container can store.
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public static int MaxArea(int[] height)
    {
        int max = 0;
        int left = 0;
        int right = height.Length - 1;
        
        while (left < right)
        {
            int width = right - left;
            int h;

            if (height[right] > height[left])
            {
                h = height[left];
                left++;
            }
            else
            {
                h = height[right];
                right--;
            }

            max = Math.Max(max, width * h);
        }

        return max;
    }
}