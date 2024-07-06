using AaDS.shared;

namespace AaDS.Sortings;

static class GnomeSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        int pos = 0;

        while (pos < collection.Count)
        {
            if (pos == 0 || comparer.Compare(collection[pos], collection[pos - 1]) >= 0)
            {
                pos++;
            }
            else
            {
                (collection[pos], collection[pos - 1]) = (collection[pos - 1], collection[pos]);
                pos--;
            }
        }
    }
}