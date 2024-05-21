namespace AaDS.Sortings;

static class Inversions
{
    static int inversions;
    public static int GetInversions(IList<int> collection)
    {
        inversions = 0;
        MSort(collection, 0, collection.Count - 1);
        return inversions;
    }

    static void Merge(IList<int> arr, int left, int middle, int right)
    {
        List<int> LArr = [];
        List<int> RArr = [];
   
        for (int index = left; index <= middle; index++)
            LArr.Add(arr[index]);
        for (int index = middle + 1; index <= right; index++)
            RArr.Add(arr[index]);
   
        int i = 0, j = 0, k = left;

        while (i < LArr.Count && j < RArr.Count)
        {
            if (LArr[i] <= RArr[j])
                arr[k++] = LArr[i++];
            else
            {
                arr[k++] = RArr[j++];
                inversions += LArr.Count - i;
            }
        }
            
        while (i < LArr.Count)
            arr[k++] = LArr[i++];
        while (j < RArr.Count)
            arr[k++] = RArr[j++];
 
    }
 
    static void MSort(IList<int> arr, int left, int right)
    {
        if (left >= right) return;
        int middle = (left + right) / 2;
        MSort(arr, left, middle);
        MSort(arr, middle + 1, right);
        Merge(arr, left, middle, right);
    }
}