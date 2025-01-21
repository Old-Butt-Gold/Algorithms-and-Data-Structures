namespace AaDS.Search;

public static class PairsOfSpellsAndPotions
{
    /// <summary>
    /// https://leetcode.com/problems/successful-pairs-of-spells-and-potions/description/?envType=study-plan-v2
    /// </summary>
    /// <param name="spells"></param>
    /// <param name="potions"></param>
    /// <param name="success"></param>
    /// <returns></returns>
    public static int[] SuccessfulPairs(int[] spells, int[] potions, long success)
    {
        Array.Sort(potions);

        int[] result = new int[spells.Length];

        for (int i = 0; i < result.Length; i++)
        {
            int left = 0;
            int right = potions.Length - 1;

            long spell = spells[i];

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (spell * potions[mid] < success)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            result[i] = potions.Length - left;
        }

        return result;
    }
}