namespace AaDS.Arrays;

public static class LexicographicallySmallestArray
{
    /// <summary>
    /// https://leetcode.com/problems/make-lexicographically-smallest-array-by-swapping-elements/description/?envType=daily-question
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public static int[] MakeSmallestArray(int[] nums, int limit)
    {
        var numsSorted = new int[nums.Length];
        Array.Copy(nums, numsSorted, nums.Length);
        Array.Sort(numsSorted);
    
        int currGroup = 0;
        var numToGroup = new Dictionary<int, int>();
        var groupToList = new Dictionary<int, Queue<int>>();

        numToGroup[numsSorted[0]] = currGroup;
        groupToList[currGroup] = [];
        groupToList[currGroup].Enqueue(numsSorted[0]);

        for (int i = 1; i < numsSorted.Length; i++)
        {
            var currentNum = numsSorted[i];
            if (currentNum - numsSorted[i - 1] > limit)
            {
                currGroup++;
                groupToList[currGroup] = [];
            }

            numToGroup[currentNum] = currGroup;
            groupToList[currGroup].Enqueue(currentNum);
        }

        for (int i = 0; i < nums.Length; i++)
        {
            int group = numToGroup[nums[i]];
            nums[i] = groupToList[group].Dequeue();
        }

        return nums;
    }
}