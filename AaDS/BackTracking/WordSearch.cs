using System.Text;
using AaDS.DataStructures.Shared;
using AaDS.DataStructures.Tree;

namespace AaDS.BackTracking;

public static class WordSearch
{
    /// <summary>
    /// Given an m x n board of characters and a list of strings words.
    /// Each word must be constructed from letters of sequentially adjacent cells,
    /// where adjacent cells are horizontally or vertically neighboring.
    /// The same letter cell may not be used more than once in a word.
    /// </summary>
    /// <param name="board"></param>
    /// <param name="words"></param>
    /// <returns>all words on the board</returns>
    public static IList<string> FindWords(char[][] board, string[] words) {
        TrieEnglishLetters trie = new();
        foreach (var word in words)
        {
            trie.Add(word);
        }

        int rows = board.Length;
        int cols = board[0].Length;

        HashSet<string> result = new();
        bool[,] visited = new bool[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Dfs(i, j, trie.Root, new());
            }
        }

        return result.ToList();

        void Dfs(int row, int col, TrieEnglishLettersNode? node, StringBuilder currentWord)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols || visited[row, col])
                return;

            char c = board[row][col];
            
            if (node == null || !node.Contains(c))
                return;
            
            node = node.Next(c)!;
            currentWord.Append(c);

            if (node.IsEndOfWord)
            {
                result.Add(currentWord.ToString());
            }

            visited[row, col] = true;

            Dfs(row + 1, col, node, currentWord); // Down
            Dfs(row - 1, col, node, currentWord); // Up
            Dfs(row, col + 1, node, currentWord); // Right
            Dfs(row, col - 1, node, currentWord); // Left

            currentWord.Length--;

            visited[row, col] = false;
        }
    }
    
    /// <summary>
    /// Given an m x n grid of characters board and a string word.
    /// The word can be constructed from letters of sequentially adjacent cells,
    /// where adjacent cells are horizontally or vertically neighboring.
    /// The same letter cell may not be used more than once.
    /// </summary>
    /// <param name="board"></param>
    /// <param name="word"></param>
    /// <returns>true if word exists in the grid</returns>
    public static bool Exist(char[][] board, string word)
    {
        int rows = board.Length;
        int cols = board[0].Length;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (Dfs(i, j, 0))
                {
                    return true;
                }
            }
        }

        return false;

        bool Dfs(int row, int col, int index)
        {
            if (row < 0 || row >= rows || col < 0 
                || col >= cols || board[row][col] != word[index])
            {
                return false;
            }

            if (index == word.Length - 1) return true;

            char temp = board[row][col]; //mark cell as visited
            board[row][col] = '#';

            bool found = Dfs(row + 1, col, index + 1) || // Down
                         Dfs(row - 1, col, index + 1) || // Up
                         Dfs(row, col + 1, index + 1) || // Right
                         Dfs(row, col - 1, index + 1); // Left

            board[row][col] = temp;

            return found;
        }
    }
}