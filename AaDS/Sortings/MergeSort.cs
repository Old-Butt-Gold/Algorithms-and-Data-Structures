using AaDS.shared;

namespace AaDS.Sortings;

static class MergeSort<T> where T : IComparable<T>
{
    static CustomComparer<T> _comparer;
    
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        MSort(collection, 0, collection.Count - 1);
    }

    static void Merge(IList<T> arr, int left, int middle, int right)
    {
        List<T> LArr = [];
        List<T> RArr = [];
   
        for (int index = left; index <= middle; index++)
            LArr.Add(arr[index]);
        for (int index = middle + 1; index <= right; index++)
            RArr.Add(arr[index]);
   
        int i = 0, j = 0, k = left;

        while (i < LArr.Count && j < RArr.Count)
            arr[k++] = _comparer.Compare(LArr[i], RArr[j]) <= 0 ? LArr[i++] : RArr[j++];
        
        while (i < LArr.Count)
            arr[k++] = LArr[i++];
        while (j < RArr.Count)
            arr[k++] = RArr[j++];
 
    }
 
    static void MSort(IList<T> arr, int left, int right)
    {
        if (left >= right) return;
        int middle = (left + right) / 2;
        MSort(arr, left, middle);
        MSort(arr, middle + 1, right);
        Merge(arr, left, middle, right);
    }
}