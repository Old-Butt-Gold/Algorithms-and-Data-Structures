using AaDS.shared;

namespace AaDS.Sortings;

static class HeapSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        for (int i = collection.Count / 2; i > -1; i--)
        {
            Heapify(collection, collection.Count, i, comparer);
        }
        
        for (int i = collection.Count - 1; i > -1; i--)
        {
            (collection[0], collection[i]) = (collection[i], collection[0]);
            Heapify(collection, i, 0, comparer);
        }
    }

    static void Heapify(IList<T> arr, int n, int i, CustomComparer<T> comparer)
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