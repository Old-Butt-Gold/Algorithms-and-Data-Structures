namespace AaDS.Arrays;

public static class TotalCostToHireKWorkers
{
    /// <summary>
    /// https://leetcode.com/problems/total-cost-to-hire-k-workers/?envType=study-plan-v2
    /// </summary>
    /// <param name="costs"></param>
    /// <param name="k"></param>
    /// <param name="candidates"></param>
    /// <returns></returns>
    public static long TotalCost(int[] costs, int k, int candidates)
    {
        var pq1 = new PriorityQueue<int, int>();
        var pq2 = new PriorityQueue<int, int>();
        int i = 0, j = costs.Length - 1;
        long totalCost = 0;

        while (k > 0)
        {
            while (pq1.Count < candidates && i <= j)
            {
                pq1.Enqueue(costs[i], costs[i]);
                i++;
            }

            while (pq2.Count < candidates && j >= i)
            {
                pq2.Enqueue(costs[j], costs[j]);
                j--;
            }

            int p1 = pq1.Count > 0 ? pq1.Peek() : int.MaxValue;
            int p2 = pq2.Count > 0 ? pq2.Peek() : int.MaxValue;

            if (p1 <= p2)
            {
                totalCost += p1;
                pq1.Dequeue();
            }
            else
            {
                totalCost += p2;
                pq2.Dequeue();
            }

            k--;
        }

        return totalCost;
    }
}