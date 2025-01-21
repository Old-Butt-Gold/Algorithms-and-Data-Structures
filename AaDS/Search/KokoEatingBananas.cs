namespace AaDS.Search;

public static class KokoEatingBananas
{
    /// <summary>
    /// https://leetcode.com/problems/koko-eating-bananas/description/?envType=study-plan-v2
    /// </summary>
    /// <param name="piles"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public static int MinEatingSpeed(int[] piles, int h)
    {
        int left = 1;
        int right = piles.Max();

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (CanEatInTime(piles, mid, h))
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return left;

        static bool CanEatInTime(int[] piles, int k, int h)
        {
            long hours = 0;

            foreach (var pile in piles)
            {
                hours += (long)Math.Ceiling(pile * 1.0 / k);
            }

            return hours <= h;
        }
    }
}