namespace AaDS.Combinatorics;

/// <summary>
/// Combination generator (nCk).
/// </summary>
static class Combination
{
    //
    public static List<List<T>> Generate<T>(IList<T> elements, int length, bool allowRepetition = false)
    {
        var result = new List<List<T>>();
        Recurse(elements, length, allowRepetition, 0, [], [], result);
        return result;
    }

    static void Recurse<T>(IList<T> elements, int length, bool allowRepetition, int currentIndex, List<T> currentCombination, HashSet<int> usedIndices, List<List<T>> result)
    {
        if (currentCombination.Count == length)
        {
            result.Add(new(currentCombination));
            return;
        }

        for (var i = currentIndex; i < elements.Count; i++)
        {
            if (usedIndices.Contains(i) && !allowRepetition) continue;

            currentCombination.Add(elements[i]);
            usedIndices.Add(i);

            Recurse(elements, length, allowRepetition, i, currentCombination, usedIndices, result);

            currentCombination.RemoveAt(currentCombination.Count - 1);
            usedIndices.Remove(i);
        }
    }
}