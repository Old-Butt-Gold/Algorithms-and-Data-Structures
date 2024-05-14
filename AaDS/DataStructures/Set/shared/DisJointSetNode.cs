namespace AaDS.DataStructures.Set.shared;

internal class DisJointSetNode<T>
{
    public T Data { get; set; }
    public int Rank { get; set; }
    public int Size;

    public DisJointSetNode<T> Parent { get; set; }

}