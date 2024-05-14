namespace AaDS.Combinatorics;

/// <summary>
///     Subset generator.
/// </summary>
static class Subset
{
    public static List<List<T>> Generate<T>(List<T> input)
    {
        var result = new List<List<T>>();

        Recurse(0, new());

        return result;
        
        void Recurse(int counter, List<T> prefix)
        {
            result.Add(new(prefix));

            for (int j = counter; j < input.Count; j++)
            {
                prefix.Add(input[j]);
                Recurse(j + 1, prefix);
                prefix.RemoveAt(prefix.Count - 1);
            }
        }
    }
}