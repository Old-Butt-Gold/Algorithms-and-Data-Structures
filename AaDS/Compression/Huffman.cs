using System.Text;
using AaDS.DataStructures.Queue;

namespace AaDS.Compression;

static class Huffman
{
    class HuffmanNode : IComparable<HuffmanNode>
    {
        public char Symbol { get; init; }
        public int Frequency { get; init; }
        public HuffmanNode? Left { get; init; }
        public HuffmanNode? Right { get; init; }
    
        public int CompareTo(HuffmanNode? other) => Frequency.CompareTo(other!.Frequency);

        public override string ToString() => $"Frequency: {Frequency}; Symbol: {Symbol}";
    }
    
    public static Dictionary<char, string> Encode(string text)
    {
        Dictionary<char, int> frequencyTable = new Dictionary<char, int>();
        foreach (char c in text)
            frequencyTable[c] = frequencyTable.ContainsKey(c) ? frequencyTable[c]++ : 1;

        PriorityQueue<HuffmanNode> priorityQueue = new();
        foreach (var kvp in frequencyTable)
            priorityQueue.Enqueue(new HuffmanNode { Symbol = kvp.Key, Frequency = kvp.Value });

        while (priorityQueue.Count > 1)
        {
            HuffmanNode left = priorityQueue.Dequeue();
            HuffmanNode right = priorityQueue.Dequeue();

            HuffmanNode parent = new HuffmanNode
            {
                Symbol = '\0',
                Frequency = left.Frequency + right.Frequency,
                Left = left,
                Right = right
            };
            priorityQueue.Enqueue(parent);
        }

        HuffmanNode root = priorityQueue.Dequeue();
        Dictionary<char, string> encodingTable = new Dictionary<char, string>();
        StringBuilder sb = new();
        BuildEncodingTable(root, sb, encodingTable);

        return encodingTable;
    }

    static void BuildEncodingTable(HuffmanNode? node, StringBuilder sb, Dictionary<char, string> encodingTable)
    {
        if (node == null)
            return;

        if (node.Symbol != '\0')
        {
            encodingTable[node.Symbol] = sb.ToString();
            return;
        }

        BuildEncodingTable(node.Left, sb.Append("0"), encodingTable);
        sb.Remove(sb.Length - 1, 1);
        BuildEncodingTable(node.Right, sb.Append("1"), encodingTable);
        sb.Remove(sb.Length - 1, 1);
    }
    
    public static string EncodeText(string text, Dictionary<char, string> encodingTable)
    {
        StringBuilder encodedText = new StringBuilder();
        foreach (char c in text)
            if (encodingTable.TryGetValue(c, out var value))
                encodedText.Append(value);
        return encodedText.ToString();
    }
    
    public static string Decode(Dictionary<char, string> encodingTable, string encodedText)
    {
        Dictionary<string, char> reverseEncodingTable = new Dictionary<string, char>();

        foreach (var kvp in encodingTable)
            reverseEncodingTable[kvp.Value] = kvp.Key;

        StringBuilder decodedText = new();
        StringBuilder currentCode = new();

        foreach (char bit in encodedText)
        {
            currentCode.Append(bit);
            if (reverseEncodingTable.TryGetValue(currentCode.ToString(), out char decodedChar))
            {
                decodedText.Append(decodedChar);
                currentCode.Clear();
            }
        }

        return decodedText.ToString();
    }

}