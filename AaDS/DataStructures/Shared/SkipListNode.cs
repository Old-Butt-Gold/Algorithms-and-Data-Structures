namespace AaDS.DataStructures.Shared;

public class SkipListNode<T>
{
    public T Value { get; set; }
    public SkipListNode<T>?[] Forward { get; set; }

    public SkipListNode(T value, int level)
    {
        Value = value;
        Forward = new SkipListNode<T>[level + 1];
    }
}