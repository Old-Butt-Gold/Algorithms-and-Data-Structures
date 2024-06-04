using AaDS.DataStructures.Heap;
using AaDS.shared;

namespace AaDS.Search;

public class FindKthLargest
{
    /// <summary>
    /// Given an integer array nums and an integer k, return the kth largest element in the array.
    /// Note that it is the kth largest element in the sorted order, not the kth distinct element.
    /// Can you solve it without sorting?
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int FindKth(int[] nums, int k)
    {
        BinaryHeap<int> heap = new();
        for (int i = 0; i < k; i++)
        {
            heap.Enqueue(nums[i]);
        }

        for (int i = k; i < nums.Length; i++)
        {
            if (nums[i] > heap.Peek())
            {
                heap.Dequeue();
                heap.Enqueue(nums[i]);
            }
        }

        return heap.Peek();
    }

    public int FindKthLargestI(int[] nums, int k) => QuickSelect<int>.QuickSel(nums, k, SortDirection.Descending);
}