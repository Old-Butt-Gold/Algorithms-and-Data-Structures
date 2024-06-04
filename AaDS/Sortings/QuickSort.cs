using AaDS.shared;

namespace AaDS.Sortings;

static class QuickSort<T> where T : IComparable<T>
{
    static CustomComparer<T> _comparer;

    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        QSort(collection, 0, collection.Count - 1);
    }

    static int Partition(IList<T> list, int left, int right)
    {
        var pivot = list[left];
        int j = left;

        for (int i = left + 1; i <= right; i++)
        {
            if (_comparer.Compare(list[i], pivot) <= 0)
            {
                j++;
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        (list[left], list[j]) = (list[j], list[left]);
        return j;
    }

    static void QSort(IList<T> list, int left, int right)
    {
        while (left < right)
        {
            int pivotIndex = Partition(list, left, right);
            if (pivotIndex - left <= right - pivotIndex)
            {
                QSort(list, left, pivotIndex - 1);
                left = pivotIndex + 1;
            }
            else
            {
                QSort(list, pivotIndex + 1, right);
                right = pivotIndex - 1;
            }
        }
    }
}