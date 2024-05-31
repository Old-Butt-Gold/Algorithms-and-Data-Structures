using AaDS.shared;

namespace AaDS.Sortings;

static class CountingSort
{
    public static void Sort(IList<int> list, SortDirection sortDirection = SortDirection.Ascending)
    {
        int max = list.Max();
        int min = list.Min();

        int[] counting = new int[max - min + 1];

        foreach (var item in list)
            counting[item - min]++;

        if (sortDirection == SortDirection.Ascending)
        {
            int index = 0;
            for (int i = 0; i < counting.Length; i++)
            {
                while (counting[i]-- > 0)
                {
                    list[index++] = i + min;
                }
            }
        }
        else
        {
            int index = 0;
            for (int i = counting.Length - 1; i >= 0; i--)
            {
                while (counting[i]-- > 0)
                {
                    list[index++] = i + min;
                }
            }
        }
    }
    
}