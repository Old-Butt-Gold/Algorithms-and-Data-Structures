using AaDS.shared;

namespace AaDS.Sortings;

static class BubbleSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        var swapped = true;
        int counter = 0;

        do
        {
            swapped = false;

            for (var i = 0; i < collection.Count - 1 - counter; i++)
            {
                if (comparer.Compare(collection[i], collection[i + 1]) > 0)
                {
                    (collection[i], collection[i + 1]) = (collection[i + 1], collection[i]);
                    swapped = true;
                }
            }

            counter++;
        } while (swapped);
    }
}