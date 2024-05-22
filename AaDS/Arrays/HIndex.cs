using AaDS.Sortings;

namespace AaDS.Arrays;

public class HIndex
{
    /// <summary>
    /// Given an array of integers citations where citations[i] is the number of citations a researcher received for their ith paper, return the researcher's h-index.
    /// </summary>
    /// <param name="citations"></param>
    /// <returns></returns>
    public static int HIndexSearch(int[] citations)
    {
        CountingSort.Sort(citations);

        for (int h = 0; h < citations.Length; h++)
        {
            if (citations[h] < h + 1)
            {
                return h;
            }
        }

        return citations.Length;
    }
}