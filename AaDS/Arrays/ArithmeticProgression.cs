using AaDS.Sortings;

namespace AaDS.Arrays;

public static class ArithmeticProgression
{
    /// <summary>
    /// A sequence of numbers is called an arithmetic progression if the difference between any two consecutive elements is the same.
    /// Given an array of numbers arr, return true if the array can be rearranged to form an arithmetic progression. Otherwise, return false.
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public static bool CanMakeArithmeticProgression(int[] arr)
    {
        CountingSort.Sort(arr);
        Array.Sort(arr);
        int diff = arr[1] - arr[0];
        for (int i = 2; i < arr.Length; i++)
        {
            if (arr[i] != arr[i - 1] + diff)
                return false;
        }

        return true;
    }
}