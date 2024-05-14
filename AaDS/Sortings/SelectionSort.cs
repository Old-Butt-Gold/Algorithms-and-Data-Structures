using AaDS.shared;

namespace AaDS.Sortings;

class SelectionSort<T> where T : IComparable<T>
{
    public static List<T> Sort(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        List<T> arr = collection.ToList();

        for (int i = 0; i < arr.Count - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < arr.Count; j++)
            {
                if (comparer.Compare(arr[j], arr[minIndex]) < 0)
                    minIndex = j;
            }

            if (minIndex != i)
                (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
        }

        return arr;
    }
}