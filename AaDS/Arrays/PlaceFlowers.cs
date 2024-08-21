namespace AaDS.Arrays;

public class PlaceFlowers
{
    /// <summary>
    /// https://leetcode.com/problems/can-place-flowers/description/?envType=study-plan-v2&envId=leetcode-75
    /// </summary>
    /// <param name="flowerbed"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        int count = 0;
        for (int i = 0; i < flowerbed.Length; i++)
        {
            if (flowerbed[i] == 0 && (i == 0 || flowerbed[i - 1] == 0) &&
                (i == flowerbed.Length - 1 || flowerbed[i + 1] == 0))
            {
                flowerbed[i] = 1;
                count++;
            }
        }

        return count >= n;
    }
}