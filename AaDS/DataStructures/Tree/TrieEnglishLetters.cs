using System.Text;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Tree;

public class TrieEnglishLetters
{
    private readonly TrieEnglishLettersNode _root = new('\0');
    
    public int UniqueWordsCount { get; private set; }
    public long TotalWordsCount { get; private set; } 
    
    public bool IsEmpty() => UniqueWordsCount == 0;

    public TrieEnglishLettersNode Root => _root;

    public void Clear()
    {
        for (int i = 0; i < 26; i++)
        {
            _root.Put((char)('a' + i), null!);
        }
        UniqueWordsCount = 0;
        TotalWordsCount = 0;
    }
    
    public void Add(string word)
    {
        ArgumentNullException.ThrowIfNull(word);

        var current = _root;
        foreach (var c in word)
        {
            if (!current.Contains(c))
            {
                current.Put(c, new(c));
            }
            current = current.Next(c)!;
            current.PrefixCount++;
        }
        
        if (!current.IsEndOfWord)
        {
            current.IsEndOfWord = true;
            UniqueWordsCount++;
        }

        current.WordCount++;
        TotalWordsCount++;
    }
    
    TrieEnglishLettersNode? SearchNode(string prefix)
    {
        var current = _root;
        foreach (var c in prefix)
        {
            if (!current.Contains(c))
            {
                return null;
            }
            current = current.Next(c)!;
        }
        return current;
    }
    
    public bool Contains(string word)
    {
        ArgumentNullException.ThrowIfNull(word);
        
        var node = SearchNode(word);
        return node is not null && node.IsEndOfWord;
    }
    
    public bool StartsWithPrefix(string prefix)
    {
        ArgumentNullException.ThrowIfNull(prefix);
        return SearchNode(prefix) is not null;
    }

    public long CountPrefix(string prefix)
    {
        ArgumentNullException.ThrowIfNull(prefix);
        return  SearchNode(prefix)?.PrefixCount ?? 0;
    }
    
    public bool Remove(string word)
    {
        ArgumentNullException.ThrowIfNull(word, nameof(word));

        var current = _root;
        var nodesStack = new Stack<TrieEnglishLettersNode>();
        foreach (var c in word)
        {
            if (current.Contains(c))
            {
                nodesStack.Push(current);
                current = current.Next(c)!;
            }
            else
            {
                return false;
            }
        }

        if (current.IsEndOfWord)
        {
            current.WordCount--;
            TotalWordsCount--;
            
            if (current.WordCount == 0)
            {
                current.IsEndOfWord = false;
                UniqueWordsCount--;
            }
            
            while (nodesStack.Count > 0)
            {
                current.PrefixCount--;
                var parent = nodesStack.Pop();
                if (current is { IsEndOfWord: false } && !HasChildren(current))
                {
                    parent.Put(current.Symbol, null!);
                }
                current = parent;
            }
            return true;
        }

        return false;
        
        bool HasChildren(TrieEnglishLettersNode node) => node.GetChildren().Any();
    }
    
    public List<string> GetWords()
    {
        List<string> words = [];
        StringBuilder currentWord = new();
        GetAllWordsFromNode(_root);
        return words;
        
        void GetAllWordsFromNode(TrieEnglishLettersNode node)
        {
            if (node.IsEndOfWord)
            {
                for (int i = 0; i < node.WordCount; i++)
                {
                    words.Add(currentWord.ToString());
                }
            }

            foreach (var (c, child) in node.GetChildren())
            {
                currentWord.Append(c);
                GetAllWordsFromNode(child);
                currentWord.Length--;
            }
        }
    }
}