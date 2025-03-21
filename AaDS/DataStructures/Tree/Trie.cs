﻿using System.Text;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Tree;

class Trie
{
    readonly TrieNode _root = new('\0');
    public int Count { get; private set; }

    public bool IsEmpty() => Count == 0;

    public void Clear()
    {
        ClearNode(_root);
        Count = 0;
        
        void ClearNode(TrieNode node)
        {
            foreach (var child in node.Children.Values)
            {
                ClearNode(child);
            }

            node.Children.Clear();
            node.IsEndOfWord = false;
        }
    }
    
    public string GetLongestCommonPrefix()
    {
        var current = _root;
        var prefix = new StringBuilder(string.Empty);

        while (current.Children.Count == 1 && !current.IsEndOfWord)
        {
            foreach (var kvp in current.Children)
            {
                prefix.Append(kvp.Key);
                current = kvp.Value;
            }
        }

        return prefix.ToString();
    }

    public void Add(string word)
    {
        ArgumentNullException.ThrowIfNull(word);

        var current = _root;
        foreach (char c in word)
        {
            if (!current.Children.TryGetValue(c, out var child))
            {
                child = new(c);
                current.Children[c] = child;
            }

            current = child;
        }

        if (!current.IsEndOfWord)
        {
            current.IsEndOfWord = true;
            Count++;
        }
    }
    
    TrieNode? SearchNode(string prefix)
    {
        var current = _root;
        foreach (char c in prefix)
        {
            if (current.Children.TryGetValue(c, out var child))
            {
                current = child;
            }
            else
            {
                return null;
            }
        }
        return current;
    }

    public string ContainsPart(string word)
    {
        var current = _root;
        for (int i = 0; i < word.Length; i++)
        {
            if (current.Children.TryGetValue(word[i], out var child))
            {
                if (child.IsEndOfWord)
                {
                    return word.Substring(0, i + 1);
                }

                current = child;
            }
            else
            {
                break;
            }
        }

        return word;
    }

    /// <summary>
    /// Determines whether the trie contains the specified word.
    /// </summary>
    /// <param name="word">The word to search for.</param>
    /// <returns>True if the trie contains the word, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input word is null.</exception>
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

    public bool Remove(string word)
    {
        ArgumentNullException.ThrowIfNull(word);

        var current = _root;
        var nodesStack = new Stack<TrieNode>();
        foreach (char c in word)
        {
            if (current.Children.TryGetValue(c, out var nextNode))
            {
                nodesStack.Push(current);
                current = nextNode;
            }
            else
            {
                return false;
            }
        }

        if (current.IsEndOfWord)
        {
            current.IsEndOfWord = false;
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

    void GetAllWordsFromNode(TrieNode node, StringBuilder currentWord, List<string> wordValuePairs, int count)
    {
        if (wordValuePairs.Count >= count) return;
        
        if (node.IsEndOfWord)
        {
            wordValuePairs.Add(currentWord.ToString());
        }

        foreach (var kvp in node.Children)
        {
            currentWord.Append(kvp.Key);
            GetAllWordsFromNode(kvp.Value, currentWord, wordValuePairs, count);
            currentWord.Length--;
        }
    }
    
    /// <summary>
    /// Provides a list of words that have the specified prefix.
    /// </summary>
    /// <param name="prefix">The prefix to search for.</param>
    /// <param name="count">Count of words to return from trie</param>
    /// <returns>A list of words that have the specified prefix.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input prefix is null.</exception>
    public List<string> GetWordsWithPrefix(string prefix, int count)
    {
        ArgumentNullException.ThrowIfNull(prefix);

        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "maxCount must be greater than 0.");
        }

        List<string> words = [];
        var prefixNode = SearchNode(prefix);

        if (prefixNode != null)
        {
            GetAllWordsFromNode(prefixNode, new (prefix), words, count);
        }

        return words;
    }

    public List<string> GetWordsWithPrefix(string prefix) => GetWordsWithPrefix(prefix, Count);

    public List<string> GetWordsWithValues() => GetWordsWithValues(Count);
    
    public List<string> GetWordsWithValues(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "maxCount must be greater than 0.");
        }
        
        List<string> wordValuePairs = [];
        GetAllWordsFromNode(_root, new (), wordValuePairs, count);
        return wordValuePairs;
    }
    
    /// <summary>
    /// Provides search by using a pattern, where '.' represents any symbol.
    /// </summary>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>True if there is any string in the data structure that matches the pattern, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input pattern is null.</exception>
    public bool ContainsPattern(string pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        return SearchWord(_root, 0);
        
        bool SearchWord(TrieNode node, int index)
        {
            if (index == pattern.Length) return node.IsEndOfWord;

            char c = pattern[index];
            return c == '.'
                ? node.Children.Values.Any(child => SearchWord(child, index + 1))
                : node.Children.TryGetValue(c, out var nextNode) && SearchWord(nextNode, index + 1);
        }
    }
    
    /// <summary>
    /// Determines whether there is any word in the trie that starts with the given pattern,
    /// where '.' can match any letter.
    /// </summary>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>True if there is any word in the trie that matches the pattern, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input pattern is null.</exception>
    public bool StartsWithPrefixPattern(string pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        return SearchPattern(_root, 0);

        bool SearchPattern(TrieNode node, int index)
        {
            if (index == pattern.Length) return true;

            char c = pattern[index];
            return c == '.'
                ? node.Children.Values.Any(child => SearchPattern(child, index + 1))
                : node.Children.TryGetValue(c, out var nextNode) && SearchPattern(nextNode, index + 1);
        }
    }
}