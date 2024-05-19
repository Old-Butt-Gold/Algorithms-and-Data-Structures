namespace AaDS.String;

public static class Construct
{
    /// <summary>
    /// Given two strings ransomNote and magazine,
    /// return true if ransomNote can be constructed by using
    /// the letters from magazine and false otherwise.
    /// </summary>
    /// <param name="ransomNote"></param>
    /// <param name="magazine"></param>
    public static bool CanConstruct(string ransomNote, string magazine) {
        Dictionary<char, int> dictionary = new();
        foreach (var item in magazine) {
            if (!dictionary.TryAdd(item, 1))
            {
                dictionary[item] += 1;
            }
        }

        foreach (var item in ransomNote) {
            if (!dictionary.ContainsKey(item)) return false;
            dictionary[item] -= 1;
            if (dictionary[item] == -1) {
                return false;
            }
        }

        return true;
    }
}