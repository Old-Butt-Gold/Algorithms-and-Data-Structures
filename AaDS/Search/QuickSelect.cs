using AaDS.shared;

namespace AaDS.Search;

class QuickSelect<T>
{
    static IComparer<T> _comparer;
    
    public static T QuickSel(IList<T> list, int k, SortDirection sortDirection = SortDirection.Ascending)
    {
        if (k < 1 || k > list.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(k), "k must be in the range 1 to list.Count");
        }
        
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        return QuickSel(list, 0, list.Count - 1, k - 1);
    }
    
    static T QuickSel(IList<T> arr, int left, int right, int k)
    {
        if (left == right)
        {
            return arr[left];
        }

        int pivotIndex = Partition(arr, left, right);
        if (k == pivotIndex)
        {
            return arr[k];
        }
        else if (k < pivotIndex)
        {
            return QuickSel(arr, left, pivotIndex - 1, k);
        }
        else
        {
            return QuickSel(arr, pivotIndex + 1, right, k);
        }
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
    
}