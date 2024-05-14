using AaDS.shared;

namespace AaDS.Sortings;

class ShellSort<T> where T : IComparable<T>
{
    public static List<T> Sort(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        CustomComparer<T> comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        List<T> arr = collection.ToList();

        int gap = arr.Count / 2;

        while (gap >= 1)
        {
            for (int i = gap; i < arr.Count; i++)
            {
                T key = arr[i];
                int j = i - gap;

                while (j >= 0 && comparer.Compare(arr[j], key) > 0)
                {
                    arr[j + gap] = arr[j];
                    j -= gap;
                }

                arr[j + gap] = key;
            }

            gap /= 2;
        }

        return arr;
    }
}