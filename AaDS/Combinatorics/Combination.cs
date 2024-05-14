namespace AaDS.Combinatorics;

/// <summary>
/// Combination generator (nCr).
/// </summary>
static class Combination
{
    public static List<List<T>> GenerateCombinations<T>(List<T> elements, int r, bool repeat = false)
    {
        var result = new List<List<T>>();
        Recurse(elements, r, repeat, 0, new(), new(), result);
        return result;
    }

    static void Recurse<T>(List<T> elements, int r, bool withRepetition,
        int currentIndex, List<T> currentCombination, HashSet<int> usedIndices, List<List<T>> result)
    {
        if (currentCombination.Count == r)
        {
            result.Add(new(currentCombination));
            return;
        }

        for (var i = currentIndex; i < elements.Count; i++)
        {
            if (usedIndices.Contains(i) && !withRepetition) continue;

            currentCombination.Add(elements[i]);
            usedIndices.Add(i);

            Recurse(elements, r, withRepetition, i, currentCombination, usedIndices, result);

            currentCombination.RemoveAt(currentCombination.Count - 1);
            usedIndices.Remove(i);
        }
    }
}