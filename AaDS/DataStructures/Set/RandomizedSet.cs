namespace AaDS.DataStructures.Set;

public class RandomizedSet<T> where T : IComparable<T> 
{

    Random _random = new();
    Dictionary<T, int> _dictionary = [];
    List<T> _list = [];

    public RandomizedSet() {
        
    }
    
    public bool Insert(T val) {
        if (_dictionary.ContainsKey(val)) {
            return false;
        }
        _list.Add(val);
        _dictionary[val] = _list.Count - 1;
        return true;
    }
    
    public bool Remove(T val) {
        if (!_dictionary.TryGetValue(val, out var index)) {
            return false;
        }

        T lastElement = _list[^1];

        // Swap the last element with the element to be removed
        _list[index] = lastElement;
        _dictionary[lastElement] = index;

        // Remove the last element
        _list.RemoveAt(_list.Count - 1);
        _dictionary.Remove(val);

        return true;
    }
    
    public T GetRandom() => _list[_random.Next(0, _list.Count)];
}