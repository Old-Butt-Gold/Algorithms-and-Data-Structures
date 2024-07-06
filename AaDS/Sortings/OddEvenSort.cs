using AaDS.shared;

namespace AaDS.Sortings;

static class OddEvenSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        bool sorted = false;

        while (!sorted)
        {
            sorted = true;

            for (int i = 1; i < collection.Count - 1; i += 2)
            {
                if (comparer.Compare(collection[i], collection[i + 1]) > 0)
                {
                    (collection[i], collection[i + 1]) = (collection[i + 1], collection[i]);
                    sorted = false;
                }
            }

            for (int i = 0; i < collection.Count - 1; i += 2)
            {
                if (comparer.Compare(collection[i], collection[i + 1]) > 0)
                {
                    (collection[i], collection[i + 1]) = (collection[i + 1], collection[i]);
                    sorted = false;
                }
            }
        }
    }
}