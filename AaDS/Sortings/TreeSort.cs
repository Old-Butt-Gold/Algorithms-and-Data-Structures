using AaDS.DataStructures.Tree;
using AaDS.shared;

namespace AaDS.Sortings;

static class TreeSort<T> where T : IComparable<T>
{
    public static void Sort(IList<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        var tree = new AVLTree<T>(collection);

        if (sortDirection == SortDirection.Ascending)
        {
            tree.InOrderTraversal(collection);
        }
        else
        {
            tree.ReverseInOrderTraversal(collection);
        }
    }
}