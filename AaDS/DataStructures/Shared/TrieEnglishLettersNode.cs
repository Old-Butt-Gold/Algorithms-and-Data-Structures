namespace AaDS.DataStructures.Shared;

//only letters from 'a' to 'z'
public class TrieEnglishLettersNode
{
    private TrieEnglishLettersNode?[] _links = new TrieEnglishLettersNode[26];
    public char Symbol { get; }
    public bool IsEndOfWord { get; set; }
    public long WordCount { get; set; }
    public long PrefixCount { get; set; }
    
    public TrieEnglishLettersNode(char ch) => Symbol = ch;

    // Check if the character is present in the current node
    public bool Contains(char c) => _links[c - 'a'] != null;

    // Insert a new node for the character
    public void Put(char c, TrieEnglishLettersNode node) => _links[c - 'a'] = node;

    // Get the next node for the character
    public TrieEnglishLettersNode? Next(char c) => _links[c - 'a'];

    // Get all children
    public IEnumerable<(char, TrieEnglishLettersNode)> GetChildren()
    {
        for (int i = 0; i < _links.Length; i++)
        {
            if (_links[i] != null)
            {
                yield return ((char)('a' + i), _links[i]!);
            }
        }
    }
}
