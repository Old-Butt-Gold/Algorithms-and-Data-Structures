namespace AaDS.DataStructures.Shared;

class TrieNode<TValue>
{
    public char Symbol { get; }
    public TValue? Data { get; set; }
    public bool IsEndOfWord { get; set; }
    public bool IsEmpty => Children.Count == 0;
    public Dictionary<char, TrieNode<TValue>> Children { get; } = new();
    
    public TrieNode(char symbol) => Symbol = symbol;
}