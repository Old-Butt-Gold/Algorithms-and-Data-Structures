namespace AaDS.DataStructures.Shared;

public class BSTNode<TValue> 
{
    public BSTNode<TValue>? Left, Right;
    public TValue Value { get; set; }
    public BSTNode(TValue value) => Value = value;
}