using AaDS.shared;

namespace AaDS.Sortings;

static class CountingSort
{
    public static void Sort(IList<int> list, SortDirection sortDirection = SortDirection.Ascending)
    {
        int max = list.Max();
        int min = list.Min();

        int[] counting = new int[max - min + 1];

        for (int i = 0; i < list.Count; i++)
            counting[list[i] - min]++;

        int index = sortDirection == SortDirection.Ascending ? 0 : list.Count - 1;

        for (int i = 0; i < counting.Length; i++)
            while (counting[i]-- > 0)
                if (sortDirection == SortDirection.Ascending)
                    list[index++] = i + min;
                else
                    list[index--] = i + min;
    }
    
}