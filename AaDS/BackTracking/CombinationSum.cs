namespace AaDS.BackTracking;

public static class CombinationSum
{
    /// <summary>
    /// https://leetcode.com/problems/combination-sum/description/?envType=study-plan-v2
    /// </summary>
    /// <param name="candidates"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static IList<IList<int>> CombinationSumI(int[] candidates, int target)
    {
        var result = new List<IList<int>>();
        var current = new List<int>();
        FindCombinations(0, target);
        return result;

        void FindCombinations(int index, int remainingTarget)
        {
            if (remainingTarget == 0)
            {
                result.Add([..current]);
                return;
            }

            for (int i = index; i < candidates.Length; i++)
            {
                int candidate = candidates[i];

                // Skip if the number is greater than the remaining target
                if (candidate > remainingTarget) continue;

                current.Add(candidate);

                FindCombinations(i, remainingTarget - candidate); // Recursively find further combinations

                current.RemoveAt(current.Count - 1); // Backtrack by removing the last number
            }
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/combination-sum-iii/?envType=study-plan-v2
    /// </summary>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static IList<IList<int>> CombinationSumIII(int k, int n)
    {
        var result = new List<IList<int>>();
        var current = new List<int>();

        FindCombinations(1, n, 0);
        return result;

        void FindCombinations(int start, int remainingTarget, int currentCount)
        {
            if (remainingTarget == 0 && currentCount == k)
            {
                result.Add([..current]); 
                return;
            }

            for (int i = start; i <= 9; i++)
            {
                if (i > remainingTarget) break; // Skip if the number exceeds the remaining target

                current.Add(i); 

                FindCombinations(i + 1, remainingTarget - i, currentCount + 1); // Recursive call with updated parameters

                current.RemoveAt(current.Count - 1); // Backtrack by removing the last added number
            }
        }
    }

}