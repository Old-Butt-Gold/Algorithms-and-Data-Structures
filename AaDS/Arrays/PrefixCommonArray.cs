namespace AaDS.Arrays;

public static class PrefixCommonArray
{
    /// <summary>
    /// https://leetcode.com/problems/find-the-prefix-common-array-of-two-arrays/description/?envType=daily-question
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static int[] FindThePrefixCommonArray(int[] A, int[] B)
    {
        var prefixCommonArray = new int[A.Length];
        var frequency = new int[A.Length + 1];
        int commonCount = 0;

        for (int i = 0; i < A.Length; i++)
        {
            frequency[A[i]]++;
            if (frequency[A[i]] == 2) commonCount++;

            frequency[B[i]]++;
            if (frequency[B[i]] == 2) commonCount++;

            prefixCommonArray[i] = commonCount;
        }

        return prefixCommonArray;
    }
}