namespace AaDS.DataStructures.Shared;

public class DoublyNode<T>
{
    public DoublyNode(T data) => Data = data;
    public T Data { get; set; }
    public DoublyNode<T>? Previous { get; set; }
    public DoublyNode<T>? Next { get; set; }
    public override string ToString() => $"{Data}";
}