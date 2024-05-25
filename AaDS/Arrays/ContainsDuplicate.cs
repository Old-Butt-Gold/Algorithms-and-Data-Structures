namespace AaDS.Arrays;

public class ContainsDuplicate
{
    /// <summary>
    /// Given an integer array nums, return true if any value appears at least twice in the array, and return false if every element is distinct.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static bool ContainsDuplicateI(int[] nums)
    {
        HashSet<int> result = [];
        foreach (var num in nums)
        {
            if (!result.Add(num))
            {
                return true;
            }
        }

        return false;
    }
    
    /// <summary>
    /// Given an integer array nums and an integer k, return true if there are two distinct indices i and j
    /// in the array such that nums[i] == nums[j] and abs(i - j) <= k.
    /// </summary>
    /// <param name="nums">The integer array.</param>
    /// <param name="k">The maximum allowed index difference.</param>
    /// <returns>True if such indices exist, otherwise false.</returns>
    public static bool ContainsDuplicateII(int[] nums, int k)
    {
        Dictionary<int, int> result = [];
        for (int i = 0; i < nums.Length; i++)
        {
            if (result.TryGetValue(nums[i], out var j))
            {
                if (Math.Abs(i - j) <= k)
                {
                    return true;
                }
                result[nums[i]] = i;
            }
            else
            {
                result.Add(nums[i], i);
            }
        }

        return false;
    }

    /// <summary>
    /// You are given an integer array nums and two integers indexDiff and valueDiff.
    /// Find a pair of indices (i, j) such that:
    /// i != j,
    /// abs(i - j) <= indexDiff.
    /// abs(nums[i] - nums[j]) <= valueDiff, and
    /// Return true if such pair exists or false otherwise.
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="indexDiff"></param>
    /// <param name="valueDiff"></param>
    /// <returns></returns>
    public static bool ContainsDuplicateIII(int[] nums, int indexDiff, int valueDiff)
    {
        var buckets = new Dictionary<int, int>(); // label key -> element in bucket with label key, note we only care about 
        // 1 element in a bucket, if we have more than 1, we know we have found a pair, and note bucketsize = valueDiff + 1;

        var bucketSize = valueDiff + 1; // e.g. if valueDiff = 3, then a bucket is [0, 3] and this bucket contains 4 elements
        // and any pair of element (x1, x2) from this bucket its difference should be |x1-x2| <= valueDiff = 3;

        int min = nums.Min();
        for (int i = 0; i < nums.Length; i++)
        {
            var shift = nums[i] - min;
            var label = shift / bucketSize;

            // if there already exists a bucket with the same label, it means, this bucket contains an element
            // which its difference with the current nums[i] will be satified with the 
            // valueDiff as well as the indexDiff(because the windows size is indexDiff in order word, indexDiff previous
            // elements from index i)
            if (buckets.ContainsKey(label))
            {
                return true;
            }

            // elements from left bucket can potentially have nums that its difference with current nums[i] <= valueDiff
            if (buckets.ContainsKey(label - 1) && Math.Abs(buckets[label - 1] - nums[i]) <= valueDiff)
            {
                return true;
            }

            // elements from right bucket can potentially have nums that its difference with current nums[i] <= valueDiff
            if (buckets.ContainsKey(label + 1) && Math.Abs(buckets[label + 1] - nums[i]) <= valueDiff)
            {
                return true;
            }

            // more notes to the above three if statments: 
            // [bucket-left][bucket-label][bucket-right] are the only buckets that contain numbers that can
            // pair with current nums[i] which their difference is <= valueDiff, any other buckets will not meet this requirement
            // because the bucketSize = valueDiff + 1;

            buckets[label] = nums[i];

            // slide the windows when we reach max possible size since
            // the far left element is no longer satified with the indexDiff condition with the next element position
            if (buckets.Count == indexDiff + 1)
            {
                var farLeftShift = nums[i - indexDiff] - min;
                var farLeftLabel = farLeftShift / bucketSize;
                buckets.Remove(farLeftLabel);
            }
        }

        return false;
    }
}