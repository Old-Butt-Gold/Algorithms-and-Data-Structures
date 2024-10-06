namespace AaDS.DataStructures.Cache;

public class LruCache<TK, TV>
{
    private readonly int _capacity;
    private readonly Dictionary<TK, LinkedListNode<KeyValuePair<TK, TV>>> _cacheMap = [];
    private readonly LinkedList<KeyValuePair<TK, TV>> _list = [];

    public LruCache(int capacity)
    {
        if (capacity <= 0) throw new ArgumentException("Capacity should be more than 0", nameof(capacity));
        _capacity = capacity;
    }

    public bool TryGetValue(TK key, out TV? value)
    {
        if (!_cacheMap.TryGetValue(key, out var node))
        {
            value = default;
            return false;
        }

        _list.Remove(node);
        _list.AddFirst(node);
        value = node.Value.Value;
        return true;
    }

    public void Put(TK key, TV value)
    {
        if (_cacheMap.TryGetValue(key, out var node))
        {
            _list.Remove(node);
            node.Value = new KeyValuePair<TK, TV>(key, value);
            _list.AddFirst(node);
            return;
        }

        if (_list.Count == _capacity)
        {
            var lastNode = _list.Last;
            if (lastNode != null)
            {
                _cacheMap.Remove(lastNode.Value.Key);
                _list.RemoveLast();
            }
        }

        var newNode = new LinkedListNode<KeyValuePair<TK, TV>>(new KeyValuePair<TK, TV>(key, value));
        _list.AddFirst(newNode);
        _cacheMap[key] = newNode;
    }
}