using AaDS.shared;

namespace AaDS.Sortings;

static class MergeSort<T> where T : IComparable<T>
{
    static CustomComparer<T> _comparer;
    
    public static List<T> Sort(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        List<T> arr = collection.ToList();
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        MSort(arr, 0, arr.Count - 1);
        return arr;
    }

    static void Merge(List<T> arr, int left, int middle, int right)
    {
        List<T> LArr = arr.GetRange(left, middle - left + 1);
        List<T> RArr = arr.GetRange(middle + 1, right - middle);
   
        int i = 0, j = 0, k = left;

        while (i < LArr.Count && j < RArr.Count)
            arr[k++] = _comparer.Compare(LArr[i], RArr[j]) <= 0 ? LArr[i++] : RArr[j++];

            
        while (i < LArr.Count)
            arr[k++] = LArr[i++];
        while (j < RArr.Count)
            arr[k++] = RArr[j++];
 
    }
 
    static void MSort(List<T> arr, int left, int right)
    {
        if (left >= right) return;
        int middle = (left + right) / 2;
        MSort(arr, left, middle);
        MSort(arr, middle + 1, right);
        Merge(arr, left, middle, right);
    }
}