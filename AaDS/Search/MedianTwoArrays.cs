namespace AaDS.Search;

public class MedianTwoArrays
{
    /// <summary>
    /// Given two sorted arrays nums1 and nums2 of size m and n respectively, return the median of the two sorted arrays.
    /// <remarks>
    /// The overall run time complexity should be O(log (m+n)).
    /// </remarks>
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int len1 = nums1.Length;
        int len2 = nums2.Length;

        // Обеспечим, чтобы первый массив был всегда больше или равен по длине
        if (len1 < len2)
        {
            return FindMedianSortedArrays(nums2, nums1);
        }

        int left = 0;
        int right = len2 * 2;

        while (left <= right)
        {
            int mid2 = left + (right - left) / 2;
            int mid1 = len1 + len2 - mid2;

            double L1 = (mid1 == 0) ? int.MinValue : nums1[(mid1 - 1) / 2];
            double L2 = (mid2 == 0) ? int.MinValue : nums2[(mid2 - 1) / 2];
            double R1 = (mid1 == len1 * 2) ? int.MaxValue : nums1[mid1 / 2];
            double R2 = (mid2 == len2 * 2) ? int.MaxValue : nums2[mid2 / 2];

            // Если левый элемент первого массива больше правого элемента второго
            if (L1 > R2)
            {
                left = mid2 + 1;
            }
            // Если левый элемент второго массива больше правого элемента первого
            else if (L2 > R1)
            {
                right = mid2 - 1;
            }
            // Если найдено правильное разбиение
            else
            {
                return (Math.Max(L1, L2) + Math.Min(R1, R2)) / 2;
            }
        }

        // Если массивы пустые или произошла ошибка
        throw new ArgumentException("Invalid input");
    }

}