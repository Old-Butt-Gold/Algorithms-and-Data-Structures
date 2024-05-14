using AaDS.shared;

namespace AaDS.Sortings;

static class BubbleSort<T> where T : IComparable<T>
{
    public static List<T> Sort(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        List<T> array = collection.ToList();
        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        var swapped = true;

        while (swapped)
        {
            swapped = false;

            for (var i = 0; i < array.Count - 1; i++)
            {
                if (comparer.Compare(array[i], array[i + 1]) > 0)
                {
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    swapped = true;
                }
            }
        }
        return array;
    }
}