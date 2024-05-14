using AaDS.shared;

namespace AaDS.Sortings;

class BinaryInsertionSort<T> where T : IComparable<T>
{
    public static List<T> Sort(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        var list = collection.ToList();
        for (int i = 1; i < list.Count; i++)
        {
            int index = i - 1;
            var item = list[i];

            int location = BinarySearch(item, 0, index);

            for (; index >= location; index--)
            {
                list[index + 1] = list[index];
            }

            list[index + 1] = item;
        }
        return list;

        int BinarySearch(T item, int left, int right)
        {
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (EqualityComparer<T>.Default.Equals(item, list[mid]))
                    return mid + 1;
                else if (comparer.Compare(item, list[mid]) > 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return left;
        }
    }
}