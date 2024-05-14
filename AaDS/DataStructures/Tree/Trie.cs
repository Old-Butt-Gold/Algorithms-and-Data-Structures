using System.Text;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Tree;

class Trie<TValue>
{
    readonly TrieNode<TValue> Root = new('\0');
    public int Count { get; private set; }

    public bool IsEmpty() => Count == 0;

    public void Clear()
    {
        ClearNode(Root);
        Count = 0;
    }

    void ClearNode(TrieNode<TValue> node)
    {
        foreach (var child in node.Children.Values)
            ClearNode(child);

        node.Children.Clear();
        node.IsEndOfWord = false;
        node.Data = default;
    }

    public void Add(string word, TValue data)
    {
        if (word is null || data is null)
            throw new ArgumentNullException(nameof(word));

        TrieNode<TValue> current = Root;
        foreach (char c in word)
        {
            if (!current.Children.TryGetValue(c, out _))
                current.Children[c] = new(c);
            current = current.Children[c];
        }

        if (!current.IsEndOfWord)
        {
            (current.IsEndOfWord, current.Data) = (true, data);
            Count++;
        }
    }

    public (TValue?, bool) GetData(string word)
    {
        var node = SearchNode(word);
        return node is not null && node.IsEndOfWord ? (node.Data, true) : (default, false);
    }

    public bool Contains(string word)
    {
        if (word == null)
            throw new ArgumentNullException(nameof(word));
        TrieNode<TValue>? node = SearchNode(word);
        return node is not null && node.IsEndOfWord;
    }

    public bool StartsWith(string prefix)
    {
        if (prefix == null)
            throw new ArgumentNullException(nameof(prefix));
        return SearchNode(prefix) is not null;
    }

    public bool Remove(string word)
    {
        if (word == null)
            throw new ArgumentNullException(nameof(word));

        TrieNode<TValue> current = Root;
        var nodesStack = new Stack.Stack<TrieNode<TValue>>();
        foreach (char c in word)
        {
            if (current.Children.TryGetValue(c, out var nextNode))
            {
                nodesStack.Push(current);
                current = nextNode;
            }
            else
                return false;
        }

        if (current.IsEndOfWord)
        {
            current.IsEndOfWord = false;
            current.Data = default;
            Count--;

            while (nodesStack.Count > 0 && current is { IsEndOfWord: false, Children.Count: 0 })
            {
                var parent = nodesStack.Pop();
                parent.Children.Remove(current.Symbol);
                current = parent;
            }
            return true;
        }

        return false;
    }

    public List<KeyValuePair<string, TValue>> GetWordsWithPrefix(string prefix)
    {
        if (prefix == null)
            throw new ArgumentNullException(nameof(prefix));
        List<KeyValuePair<string, TValue>> words = new();
        TrieNode<TValue>? prefixNode = SearchNode(prefix);

        if (prefixNode != null)
            GetAllWordsFromNode(prefixNode, new StringBuilder(prefix), words);

        return words;
    }

    public string GetLongestCommonPrefix(string word)
    {
        if (word == null)
            throw new ArgumentNullException(nameof(word));
        TrieNode<TValue> current = Root;
        StringBuilder commonPrefix = new();

        foreach (char c in word.ToLower())
            if (current.Children.TryGetValue(c, out _))
            {
                commonPrefix.Append(c);
                current = current.Children[c];
            }
            else
                break;

        return commonPrefix.ToString();
    }

    void GetAllWordsFromNode(TrieNode<TValue> node, StringBuilder currentWord, List<KeyValuePair<string, TValue>> wordValuePairs)
    {
        if (node.IsEndOfWord)
            wordValuePairs.Add(new(currentWord.ToString(), node.Data!));

        foreach (var kvp in node.Children)
        {
            currentWord.Append(kvp.Key);
            GetAllWordsFromNode(kvp.Value, currentWord, wordValuePairs);
            currentWord.Length--;
        }
    }

    public List<KeyValuePair<string, TValue>> GetWordsWithValues()
    {
        List<KeyValuePair<string, TValue>> wordValuePairs = new();
        GetAllWordsFromNode(Root, new StringBuilder(), wordValuePairs);
        return wordValuePairs;
    }

    TrieNode<TValue>? SearchNode(string prefix)
    {
        TrieNode<TValue>? current = Root;
        foreach (char c in prefix)
        {
            if (current.Children.TryGetValue(c, out _))
                current = current.Children[c];
            else
                return null;
        }
        return current;
    }
}