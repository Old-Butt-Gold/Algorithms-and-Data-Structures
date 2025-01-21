namespace AaDS.SlidingWindow;

public static class MaximumSubsequeceScore
{
    /// <summary>
    /// You are given two 0-indexed integer arrays nums1 and nums2 of equal length n and a positive integer k. You must choose a subsequence of indices from nums1 of length k.
    /// For chosen indices i0, i1, ..., ik - 1, your score is defined as:
    /// The sum of the selected elements from nums1 multiplied with the minimum of the selected elements from nums2.
    /// It can defined simply as: (nums1[i0] + nums1[i1] +...+ nums1[ik - 1]) * min(nums2[i0] , nums2[i1], ... ,nums2[ik - 1]).
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <param name="k"></param>
    /// <returns>the maximum possible score</returns>
    public static long MaxScore(int[] nums1, int[] nums2, int k)
    {
        int n = nums1.Length;
        var sortedPairs = new Num[n];

        for (int i = 0; i < n; i++)
        {
            sortedPairs[i] = new(nums1[i], nums2[i]);
        }

        Array.Sort(sortedPairs, (a, b) => b.Num2 - a.Num2);

        PriorityQueue<int, int> minHeap = new();
        long currentSum = 0;
        long maxScore = 0;

        // SlidingWindow
        for (int i = 0; i < k; i++)
        {
            currentSum += sortedPairs[i].Num1;
            minHeap.Enqueue(sortedPairs[i].Num1, sortedPairs[i].Num1);
        }

        maxScore = currentSum * sortedPairs[k - 1].Num2;

        for (int i = k; i < sortedPairs.Length; i++)
        {
            currentSum += sortedPairs[i].Num1 - minHeap.Dequeue();
            minHeap.Enqueue(sortedPairs[i].Num1, sortedPairs[i].Num1);

            maxScore = Math.Max(maxScore, currentSum * sortedPairs[i].Num2);
        }

        return maxScore;
    }

    private record Num(int Num1, int Num2);
}