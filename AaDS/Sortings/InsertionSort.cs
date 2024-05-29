using AaDS.shared;

namespace AaDS.Sortings;

class InsertionSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        for (int i = 1; i < collection.Count; i++)
        {
            T key = collection[i];
            int j = i - 1;
            while (j > -1 && comparer.Compare(collection[j], key) > 0)
            {
                collection[j + 1] = collection[j];
                j--;
            }
            collection[j + 1] = key;
        }
    }
}