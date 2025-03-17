namespace AaDS.DFS;

public static class EventualSafeStates
{
    /// <summary>
    /// https://leetcode.com/problems/find-eventual-safe-states/description/?envType=daily-question
    /// </summary>
    /// <param name="graph"></param>
    /// <returns></returns>
    public static IList<int> EventualSafeNodes(int[][] graph)
    {
        var status = new int[graph.Length];
        var result = new List<int>();

        for (int i = 0; i < graph.Length; i++)
        {
            if (IsSafe(i, graph, status))
            {
                result.Add(i);
            }
        }

        return result;

        static bool IsSafe(int node, int[][] graph, int[] status)
        {
            // Статус узлов:
            // 0 - не посещен
            // 1 - в процессе посещения
            // 2 - безопасный узел
            if (status[node] > 0)
            {
                return status[node] == 2;
            }

            status[node] = 1;

            foreach (var neighbor in graph[node])
            {
                if (!IsSafe(neighbor, graph, status))
                {
                    return false; // Если хотя бы один сосед небезопасен, текущий узел тоже небезопасен
                }
            }

            // Узел безопасен, если все пути ведут к безопасным узлам
            status[node] = 2;
            return true;
        }
    }
}