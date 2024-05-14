using AaDS.DataStructures.Tree;
using AaDS.shared;

namespace AaDS.Sortings;

static class TreeSort<T> where T : IComparable<T>
{
    public static List<T> Sort(IEnumerable<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        AVLTree<T> tree = new AVLTree<T>(collection);
        return sortDirection == SortDirection.Ascending 
            ? tree.InOrderTraversal() 
            : tree.ReverseInOrderTraversal();
    }
}