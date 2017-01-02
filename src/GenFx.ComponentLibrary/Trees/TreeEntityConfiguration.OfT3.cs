namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents the configuration of <see cref="TreeEntity{TEntity, TConfiguration, TNode}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    /// <typeparam name="TNode">
    /// The type of nodes in the tree.  <typeparamref name="TNode"/> must be a 
    /// type of <see cref="TreeNode"/> and have a default public constructor.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class TreeEntityConfiguration<TConfiguration, TEntity, TNode> : TreeEntityConfiguration<TConfiguration, TEntity>
        where TConfiguration : TreeEntityConfiguration<TConfiguration, TEntity, TNode> 
        where TEntity : TreeEntity<TEntity, TConfiguration, TNode>
        where TNode : TreeNode, new()
    {
    }
}
