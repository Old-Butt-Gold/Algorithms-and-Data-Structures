using AaDS.shared;

namespace AaDS.Sortings;

class SelectionSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        for (int i = 0; i < collection.Count - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < collection.Count; j++)
            {
                if (comparer.Compare(collection[j], collection[minIndex]) < 0)
                    minIndex = j;
            }

            if (minIndex != i)
                (collection[i], collection[minIndex]) = (collection[minIndex], collection[i]);
        }
    }
}