namespace AaDS.Sortings;

static class Inversions
{
    static int inversions;
    public static int GetInversions(IEnumerable<int> collection)
    {
        inversions = 0;
        List<int> arr = collection.ToList();
        MSort(arr, 0, arr.Count - 1);
        return inversions;
    }

    static void Merge(List<int> arr, int left, int middle, int right)
    {
        List<int> LArr = arr.GetRange(left, middle - left + 1);
        List<int> RArr = arr.GetRange(middle + 1, right - middle);
   
        int i = 0, j = 0, k = left;

        while (i < LArr.Count && j < RArr.Count)
        {
            if (LArr[i] <= RArr[j])
                arr[k++] = LArr[i++];
            else
            {
                arr[k++] = RArr[j++];
                inversions += LArr.Count - 1;
            }
        }
            
        while (i < LArr.Count)
            arr[k++] = LArr[i++];
        while (j < RArr.Count)
            arr[k++] = RArr[j++];
 
    }
 
    static void MSort(List<int> arr, int left, int right)
    {
        if (left >= right) return;
        int middle = (left + right) / 2;
        MSort(arr, left, middle);
        MSort(arr, middle + 1, right);
        Merge(arr, left, middle, right);
    }
}