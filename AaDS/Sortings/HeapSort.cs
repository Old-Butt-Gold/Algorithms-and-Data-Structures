using AaDS.shared;

namespace AaDS.Sortings;

static class HeapSort<T> where T : IComparable<T>
{
    public static List<T> Sort(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        List<T> arr = collection.ToList();
        for (int i = arr.Count / 2 - 1; i >= 0; i--)
            Heapify(arr, arr.Count, i, comparer);
        
        for (int i = arr.Count - 1; i >= 0; i--)
        {
            (arr[0], arr[i]) = (arr[i], arr[0]);
            Heapify(arr, i, 0, comparer);
        }

        return arr;
    }

    static void Heapify(List<T> arr, int n, int i, CustomComparer<T> comparer)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && comparer.Compare(arr[left], arr[largest]) > 0)
            largest = left;

        if (right < n && comparer.Compare(arr[right], arr[largest]) > 0)
            largest = right;

        if (largest != i)
        {
            (arr[i], arr[largest]) = (arr[largest], arr[i]);
            Heapify(arr, n, largest, comparer);
        }
    }

}