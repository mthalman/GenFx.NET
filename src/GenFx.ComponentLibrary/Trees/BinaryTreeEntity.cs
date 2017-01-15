namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a binary tree.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the nodes of the tree.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class BinaryTreeEntity<TValue> : TreeEntity<BinaryTreeNode<TValue>>
    {
    }
}
