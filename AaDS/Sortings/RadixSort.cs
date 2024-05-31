using AaDS.shared;
using System.Linq;

namespace AaDS.Sortings;

static class RadixSort
{
    public static void Sort(IList<int> array, SortDirection sortDirection = SortDirection.Ascending) =>
        RadixSortIntegers(array, sortDirection);

    static void RadixSortIntegers(IList<int> array, SortDirection sortDirection)
    {
        if (array.Count < 2)
            return;

        int max = array.Max(Math.Abs);
        int exponent = 1;

        int[] output = new int[array.Count];
        int[] count = new int[19]; // from -9 to 9

        while (max / exponent > 0)
        {
            Array.Clear(count, 0, count.Length);

            foreach (var item in array)
            {
                int bucketIndex = (item / exponent % 10) + 9; // Adjusting index for negative numbers
                count[bucketIndex]++;
            }

            for (int i = 1; i < 19; i++)
            {
                count[i] += count[i - 1];
            }

            if (sortDirection == SortDirection.Ascending)
            {
                for (int i = array.Count - 1; i > -1; i--)
                {
                    int bucketIndex = (array[i] / exponent % 10) + 9;
                    output[--count[bucketIndex]] = array[i];
                }
            }
            else
            {
                for (int i = 0; i < array.Count; i++)
                {
                    int bucketIndex = (array[i] / exponent % 10) + 9;
                    output[--count[bucketIndex]] = array[i];
                }
            }

            for (int i = 0; i < array.Count; i++)
            {
                array[i] = output[i];
            }

            exponent *= 10;
        }
    }

    public static void Sort(IList<string> array, SortDirection sortDirection = SortDirection.Ascending) =>
        RadixSortStrings(array, sortDirection);

    static void RadixSortStrings(IList<string> array, SortDirection sortDirection)
    {
        if (array.Count < 2)
            return;

        int maxLength = array.Max(str => str.Length);
        int maxChar = array.SelectMany(str => str).Max() + 1;

        string[] output = new string[array.Count];
        int[] count = new int[maxChar];

        for (int digit = maxLength - 1; digit > -1; digit--)
        {
            Array.Clear(count, 0, count.Length);

            foreach (var str in array)
            {
                int bucketIndex = digit < str.Length ? str[digit] : 0; // Use 0 for missing characters
                count[bucketIndex]++;
            }

            for (int i = 1; i < maxChar; i++)
            {
                count[i] += count[i - 1];
            }

            if (sortDirection == SortDirection.Ascending)
            {
                for (int i = array.Count - 1; i > -1; i--)
                {
                    int bucketIndex = digit < array[i].Length ? array[i][digit] : 0;
                    output[--count[bucketIndex]] = array[i];
                }
            }
            else
            {
                for (int i = 0; i < array.Count; i++)
                {
                    int bucketIndex = digit < array[i].Length ? array[i][digit] : 0;
                    output[--count[bucketIndex]] = array[i];
                }
            }

            for (int i = 0; i < array.Count; i++)
            {
                array[i] = output[i];
            }
        }
    }
}
