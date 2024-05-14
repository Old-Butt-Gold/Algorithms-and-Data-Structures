namespace AaDS.Combinatorics;

/// <summary>
/// Permutation generator (nPr).
/// </summary>
public class Permutation
{
    public static List<List<T>> Generate<T>(List<T> elements, int r, bool repeat = false)
    {
        var result = new List<List<T>>();
        Recurse(elements, r, repeat, new List<T>(), new HashSet<int>(), result);
        return result;
    }

    static void Recurse<T>(List<T> elements, int r, bool withRepetition, List<T> currentPermutation, HashSet<int> usedIndices, List<List<T>> result)
    {
        if (currentPermutation.Count == r)
        {
            result.Add(new List<T>(currentPermutation));
            return;
        }

        for (var i = 0; i < elements.Count; i++)
        {
            if (usedIndices.Contains(i) && !withRepetition) continue;

            currentPermutation.Add(elements[i]);
            usedIndices.Add(i);

            Recurse(elements, r, withRepetition, currentPermutation, usedIndices, result);

            currentPermutation.RemoveAt(currentPermutation.Count - 1);
            usedIndices.Remove(i);
        }
    }
}