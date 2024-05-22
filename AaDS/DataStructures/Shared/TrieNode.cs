namespace AaDS.DataStructures.Shared;

class TrieNode
{
    public char Symbol { get; }
    public bool IsEndOfWord { get; set; }
    public bool IsEmpty => Children.Count == 0;
    public Dictionary<char, TrieNode> Children { get; } = new();
    
    public TrieNode(char symbol) => Symbol = symbol;
}