using AaDS.shared;

namespace AaDS.Sortings;

class InsertionSort<T> where T : IComparable<T>
{
    public static List<T> Sort(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        List<T> arr = collection.ToList();
        for (int i = 1; i < arr.Count; i++)
        {
            T key = arr[i];
            int j = i - 1;
            while (j > -1 && comparer.Compare(arr[j], key) > 0)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = key;
        }
        return arr;
    }
}