using System.Collections;
using AaDS.DataStructures.Set.shared;

//Без повторений
namespace AaDS.DataStructures.Set;

public class DisJointSet<T> : IEnumerable<T>
{
    readonly Dictionary<T, DisJointSetNode<T>> _set = new();

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<T> GetEnumerator() => _set.Values.Select(x => x.Data).GetEnumerator();

    public void MakeSet(T member)
    {
        if (_set.ContainsKey(member)) 
            throw new Exception("A set with given member already exists.");

        DisJointSetNode<T> newSet = new()
        {
            Data = member,
            Rank = 0,
            Size = 1,
        };

        newSet.Parent = newSet;
        _set.Add(member, newSet);
        Count++;
    }
    
    public T FindSet(T member)
    {
        if (!_set.ContainsKey(member)) 
            throw new Exception("No such set with given member.");

        return FindSet(_set[member]).Data;
    }

    DisJointSetNode<T> FindSet(DisJointSetNode<T> node)
    {
        var parent = node.Parent;

        if (node != parent)
        {
            node.Parent = FindSet(node.Parent);
            return node.Parent;
        }

        return parent;
    }

    public void Union(T memberA, T memberB)
    {
        var rootA = FindSet(memberA);
        var rootB = FindSet(memberB);

        if (EqualityComparer<T>.Default.Equals(rootA, rootB)) 
            return;

        var nodeA = _set[rootA];
        var nodeB = _set[rootB];

        if (nodeA.Rank == nodeB.Rank)
        {
            nodeB.Parent = nodeA;
            nodeA.Rank++;
            nodeA.Size += nodeB.Size;
        }
        else
        {
            if (nodeA.Rank < nodeB.Rank)
            {
                nodeA.Parent = nodeB;
                nodeB.Size += nodeA.Size;
            }
            else
            {
                nodeB.Parent = nodeA;
                nodeA.Size += nodeB.Size;
            }
        }
    }
    
    public bool Contains(T member) => _set.ContainsKey(member);

    public bool IsConnected(T x, T y) => EqualityComparer<T>.Default.Equals(FindSet(x), FindSet(y));

    public int ClusterSize(T member) {
        if (!_set.ContainsKey(member)) {
            throw new ArgumentException("No such set with the given member.");
        }

        return FindSet(_set[member]).Size;
    }
    
}