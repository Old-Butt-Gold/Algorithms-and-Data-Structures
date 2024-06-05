namespace AaDS.DataStructures.Shared;

public class TernaryTreeNode
{
    public char Value { get; set; }
    public TernaryTreeNode? Less { get; set; }
    public TernaryTreeNode? Equal { get; set; }
    public TernaryTreeNode? Greater { get; set; }
    public bool IsEndOfWord { get; set; }

    public TernaryTreeNode(char value) => Value = value;
}