using AaDS.shared;

namespace AaDS.Sortings;

static class ShakerSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        int left = 0;
        int right = collection.Count - 1;
        bool swapped = true;

        while (swapped)
        {
            swapped = false;
            for (int i = left; i < right; i++)
            {
                if (comparer.Compare(collection[i], collection[i + 1]) > 0)
                {
                    (collection[i], collection[i + 1]) = (collection[i + 1], collection[i]);
                    swapped = true;
                }
            }

            right--;

            if (!swapped) break;

            swapped = false;
            for (int i = right; i > left; i--)
            {
                if (comparer.Compare(collection[i], collection[i - 1]) < 0)
                {
                    (collection[i], collection[i - 1]) = (collection[i - 1], collection[i]);
                    swapped = true;
                }
            }

            left++;
        }
    }
}