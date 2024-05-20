namespace AaDS.Combinatorics;

/// <summary>
/// Permutation generator (nAk).
/// </summary>
public class Permutation
{
    //When length == elements.Count, It's just Pn (перестановки, иначе размещения)
    public static List<List<T>> Generate<T>(List<T> elements, int length, bool allowRepetition = false)
    {
        var result = new List<List<T>>();
        GeneratePermutations(elements, length, allowRepetition, [], [], result);
        return result;
    }

    static void GeneratePermutations<T>(List<T> elements, int length, bool allowRepetition, List<T> currentPermutation, HashSet<int> usedIndices, List<List<T>> result)
    {
        if (currentPermutation.Count == length)
        {
            result.Add(new List<T>(currentPermutation));
            return;
        }

        for (var i = 0; i < elements.Count; i++)
        {
            if (usedIndices.Contains(i) && !allowRepetition) continue;

            currentPermutation.Add(elements[i]);
            usedIndices.Add(i);

            GeneratePermutations(elements, length, allowRepetition, currentPermutation, usedIndices, result);

            currentPermutation.RemoveAt(currentPermutation.Count - 1);
            usedIndices.Remove(i);
        }
    }
}