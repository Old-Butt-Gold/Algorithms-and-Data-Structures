using AaDS.shared;

namespace AaDS.Sortings;

static class TimSort<T> where T : IComparable<T>
{
    const int MIN_MERGE = 32;
    static CustomComparer<T> _comparer;
    static int MinRunLength(int n)
    {
        int r = 0;
        while (n >= MIN_MERGE)
        {
            r |= n & 1;
            n >>= 1;
        }
        return n + r;
    }

    static void InsertionSort(List<T> list, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            T key = list[i];
            int j = i;
            while (j > left && _comparer.Compare(list[j - 1], key) > 0)
            {
                list[j] = list[j - 1];
                j--;
            }
            list[j] = key;
        }
    }

    static void Merge(List<T> list, int left, int middle, int right)
    {
        List<T> LArr = list.GetRange(left, middle - left + 1);
        List<T> RArr = list.GetRange(middle + 1, right - middle);

        int i = 0, j = 0, k = left;

        while (i < LArr.Count && j < RArr.Count)
            list[k++] = _comparer.Compare(LArr[i], RArr[j]) <= 0 ? LArr[i++] : RArr[j++];
        
        while (i < LArr.Count)
            list[k++] = LArr[i++];

        while (j < RArr.Count)
            list[k++] = RArr[j++];
    }

    public static List<T> Sort(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        _comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        List<T> arr = collection.ToList();
        int n = arr.Count;

        if (n < 2)
            return arr;
        
        int minRun = MinRunLength(n);

        for (int i = 0; i < n; i += minRun)
            InsertionSort(arr, i, Math.Min(i + minRun - 1, n - 1));

        for (int size = minRun; size < n; size = 2 * size)
        {
            for (int left = 0; left < n; left += 2 * size)
            {
                int mid = left + size - 1;
                int right = Math.Min((left + 2 * size - 1), (n - 1));

                if (mid < right)
                {
                    Merge(arr, left, mid, right);
                }
            }
        }

        return arr;
    }
}