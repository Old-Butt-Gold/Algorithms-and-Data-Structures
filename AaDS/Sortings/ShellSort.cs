using AaDS.shared;

namespace AaDS.Sortings;

class ShellSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        int gap = collection.Count / 2;

        while (gap >= 1)
        {
            for (int i = gap; i < collection.Count; i++)
            {
                T key = collection[i];
                int j = i - gap;

                while (j >= 0 && comparer.Compare(collection[j], key) > 0)
                {
                    collection[j + gap] = collection[j];
                    j -= gap;
                }

                collection[j + gap] = key;
            }

            gap /= 2;
        }
    }
}