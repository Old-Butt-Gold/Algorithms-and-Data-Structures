using System.Collections;
using System.Text;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.Tree;

public class TernarySearchTree : IEnumerable<string>
{
    TernaryTreeNode? _root;
    
    public int Count { get; private set; }

    public bool IsEmpty => Count == 0;
    
    public void Insert(string word)
    {
        _root = Insert(_root, word, 0);
        
        TernaryTreeNode Insert(TernaryTreeNode? node, string word, int index)
        {
            if (node == null)
            {
                node = new TernaryTreeNode(word[index]);
            }

            if (word[index] < node.Value)
            {
                node.Less = Insert(node.Less, word, index);
            }
            else if (word[index] > node.Value)
            {
                node.Greater = Insert(node.Greater, word, index);
            }
            else
            {
                if (index < word.Length - 1)
                {
                    node.Equal = Insert(node.Equal, word, index + 1);
                }
                else
                {
                    if (!node.IsEndOfWord)
                    {
                        Count++;
                    }
                    node.IsEndOfWord = true;
                }
            }

            return node;
        }
    }

    public bool Search(string word)
    {
        return Search(_root, word, 0);
        
        bool Search(TernaryTreeNode? node, string word, int index)
        {
            if (node == null)
            {
                return false;
            }

            if (word[index] < node.Value)
            {
                return Search(node.Less, word, index);
            }
            else if (word[index] > node.Value)
            {
                return Search(node.Greater, word, index);
            }
            else
            {
                if (index == word.Length - 1)
                {
                    return node.IsEndOfWord;
                }
                else
                {
                    return Search(node.Equal, word, index + 1);
                }
            }
        }
    }

    public bool StartsWith(string prefix) => FindPrefixNode(_root, prefix, 0) != null;

    public List<string> GetWordsWithPrefix(string prefix)
    {
        List<string> words = [];
        var prefixNode = FindPrefixNode(_root, prefix, 0);

        if (prefixNode != null)
        {
            // If the prefix itself is a word, add it
            if (prefixNode.IsEndOfWord)
            {
                words.Add(prefix);
            }

            Traverse(prefixNode.Equal, new StringBuilder(prefix), words);
        }

        return words;
    }
    
    TernaryTreeNode? FindPrefixNode(TernaryTreeNode? node, string prefix, int index)
    {
        if (node == null)
        {
            return null;
        }

        if (prefix[index] < node.Value)
        {
            return FindPrefixNode(node.Less, prefix, index);
        }
        else if (prefix[index] > node.Value)
        {
            return FindPrefixNode(node.Greater, prefix, index);
        }
        else
        {
            if (index == prefix.Length - 1)
            {
                return node;
            }
            else
            {
                return FindPrefixNode(node.Equal, prefix, index + 1);
            }
        }
    }
    
    public void Delete(string word)
    {
        _root = Delete(_root, word, 0);
        
        TernaryTreeNode? Delete(TernaryTreeNode? node, string word, int index)
        {
            if (node == null)
            {
                return null;
            }

            if (word[index] < node.Value)
            {
                node.Less = Delete(node.Less, word, index);
            }
            else if (word[index] > node.Value)
            {
                node.Greater = Delete(node.Greater, word, index);
            }
            else
            {
                if (index < word.Length - 1)
                {
                    node.Equal = Delete(node.Equal, word, index + 1);
                }
                else
                {
                    if (node.IsEndOfWord)
                    {
                        Count--;
                    }
                    node.IsEndOfWord = false;
                }
            }

            // Если все поддеревья пустые и это не конец слова, удаляем узел
            if (node.Less == null && node.Equal == null && node.Greater == null && !node.IsEndOfWord)
            {
                return null;
            }

            return node;
        }
    }

    public void Clear()
    {
        _root = null;
        Count = 0;
    }

    public IEnumerator<string> GetEnumerator()
    {
        List<string> list = [];
        Traverse(_root, new StringBuilder(), list);
        return list.GetEnumerator();
    }
    
    void Traverse(TernaryTreeNode? node, StringBuilder sb, List<string> list)
    {
        if (node != null)
        {
            // Посетим левое поддерево
            Traverse(node.Less, sb, list);

            // Добавим текущий символ в StringBuilder
            sb.Append(node.Value);

            // Если это конец слова, добавим его в итератор
            if (node.IsEndOfWord)
            {
                list.Add(sb.ToString());
            }

            // Посетим среднее поддерево
            Traverse(node.Equal, sb, list);

            // Удалим последний добавленный символ
            sb.Length--;

            Traverse(node.Greater, sb, list);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}