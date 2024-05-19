namespace AaDS.Sortings;

public static class MergeArray
{
    /// <summary>
    /// two sorted arrays in non-descreasing order, nums1 has length of m + n;
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="m"></param>
    /// <param name="nums2"></param>
    /// <param name="n"></param>
    public static void Merge(int[] nums1, int m, int[] nums2, int n) {
        if (n is 0) return;
        int pointerM = m - 1;
        int pointerN = n - 1;
        int i = m + n - 1;

        while (pointerN > 0)
        {
            nums1[i--] = pointerM > -1 && nums1[pointerM] > nums2[pointerN] 
                ? nums1[pointerM--] 
                : nums2[pointerN--];
        }
    }
}