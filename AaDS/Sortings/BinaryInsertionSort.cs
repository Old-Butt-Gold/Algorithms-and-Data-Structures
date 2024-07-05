using AaDS.shared;

namespace AaDS.Sortings;

class BinaryInsertionSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        for (int i = 1; i < collection.Count; i++)
        {
            int index = i - 1;
            var item = collection[i];

            int location = BinarySearch(item, 0, index);

            for (; index >= location; index--)
            {
                collection[index + 1] = collection[index];
            }

            collection[index + 1] = item;
        }

        int BinarySearch(T item, int left, int right)
        {
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (EqualityComparer<T>.Default.Equals(item, collection[mid]))
                    return mid + 1;
                else if (comparer.Compare(item, collection[mid]) > 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return left;
        }
    }
}