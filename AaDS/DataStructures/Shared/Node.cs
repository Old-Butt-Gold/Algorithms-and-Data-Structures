namespace AaDS.DataStructures.Shared;

class Node<T>
{
    public Node() { }
    
    public Node(T data) => Data = data;

    public T Data { get; set; }
    public Node<T>? Next { get; set; }
}