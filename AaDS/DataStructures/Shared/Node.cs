using System.Text;

namespace AaDS.DataStructures.Shared;

class Node<T>
{
    public Node() { }
    
    public Node(T data) => Data = data;

    public T Data { get; set; }
    public Node<T>? Next { get; set; }
    
    public override string ToString()
    {
        var current = this;
        var result = new StringBuilder();

        while (current != null)
        {
            result.Append(current.Data);
            if (current.Next != null)
            {
                result.Append(" -> ");
            }
            current = current.Next;
        }

        return result.ToString();
    }
}