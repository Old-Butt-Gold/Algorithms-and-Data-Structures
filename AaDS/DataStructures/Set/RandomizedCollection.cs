namespace AaDS.DataStructures.Set;

public class RandomizedCollection
{
    readonly Dictionary<int, HashSet<int>> _dict = [];
    readonly List<int> _list = [];
    readonly Random _rand = new();

    public bool Insert(int val)
    {
        if (!_dict.TryGetValue(val, out var indices))
        {
            indices = [];
            _dict[val] = indices;
        }

        _list.Add(val);
        int index = _list.Count - 1;
        indices.Add(index);
        return indices.Count == 1;
    }

    public bool Remove(int val)
    {
        if (!_dict.TryGetValue(val, out var indices))
        {
            return false;
        }

        int indexToRemove = indices.First();
        indices.Remove(indexToRemove);

        int lastIndex = _list.Count - 1;
        if (indexToRemove != lastIndex)
        {
            int lastVal = _list[lastIndex];
            _list[indexToRemove] = lastVal;

            _dict[lastVal].Remove(lastIndex);
            _dict[lastVal].Add(indexToRemove);
        }

        _list.RemoveAt(lastIndex);

        if (indices.Count == 0)
        {
            _dict.Remove(val);
        }

        return true;
    }

    public int GetRandom() => _list[_rand.Next(_list.Count)];
}