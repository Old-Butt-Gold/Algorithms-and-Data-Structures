namespace AaDS.DataStructures.Shared;

public class AVLNode<TValue>
{
    public TValue Value;
    public AVLNode<TValue>? Left, Right;
    public int Height = 1;

    public AVLNode(TValue value) => Value = value;
}