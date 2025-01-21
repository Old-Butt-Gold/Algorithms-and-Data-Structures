using AaDS.DataStructures.MonotonicStack;

namespace AaDS.Monotonic_Stack;

public static class NextGreaterElement
{
    /// <summary>
    /// https://leetcode.com/problems/next-greater-element-i/description/
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <returns></returns>
    public static int[] NextGreaterElementI(int[] nums1, int[] nums2)
    {
        int[] result = new int[nums1.Length];
        Array.Fill(result, -1);

        Dictionary<int, int> dict = [];
        for (int i = 0; i < nums1.Length; i++) {
            dict[nums1[i]] = i;
        }

        var monotonicStack = new MonotonicStack<(int num, int index)>((a, b) => a.num < b.num);

        for (int i = 0; i < nums2.Length; i++) {
            int currentIndex = i;
            monotonicStack.Push((nums2[i], i), tuple => {
                if (nums1.Contains(tuple.num)) {
                    result[dict[tuple.num]] = nums2[i];
                }
            });
        }

        return result;
    }

    public static int[] NextGreaterElementIWithout(int[] nums1, int[] nums2)
    {
        var result = new int[nums1.Length];
        Array.Fill(result, -1);

        Dictionary<int, int> dict = [];
        for (int i = 0; i < nums1.Length; i++) {
            dict[nums1[i]] = i;
        }
        
        var stack = new Stack<int>();

        for (int i = 0; i < nums2.Length; i++)
        {
            while (stack.Count > 0 && stack.Peek() < nums2[i])
            {
                var num = stack.Pop();
                if (nums1.Contains(num))
                {
                    result[dict[num]] = nums2[i];
                }
            }
            
            stack.Push(nums2[i]);
        }

        return result;
    }
}