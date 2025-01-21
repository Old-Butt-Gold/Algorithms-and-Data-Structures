using System.Text;
using AaDS.DataStructures.Tree;

namespace AaDS.String;

public static class SearchSuggestionSystem
{
    public static IList<IList<string>> SuggestedProducts(string[] products, string searchWord) {
        var suggestedProducts = new List<IList<string>>(searchWord.Length);
        StringBuilder sb = new();
        Trie trie = new();
        
        Array.Sort(products);
        
        foreach (var product in products)
        {
            trie.Add(product);
        }
        
        foreach (var character in searchWord)
        {
            sb.Append(character);
            suggestedProducts.Add([..trie.GetWordsWithPrefix(sb.ToString(), 3)]);
        }

        return suggestedProducts;
    }
}