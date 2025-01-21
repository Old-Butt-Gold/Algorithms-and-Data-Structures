namespace AaDS.String;

public static class FinalValue
{
    /// <summary>
    /// https://leetcode.com/problems/final-value-of-variable-after-performing-operations/description/
    /// </summary>
    /// <param name="operations"></param>
    /// <returns></returns>
    public static int FinalValueAfterOperations(string[] operations)
    {
        int x = 0;

        foreach (var operation in operations)
        {
            x = operation[1] == '+' ? x + 1 : x - 1;
        }

        return x;
    }
}