namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents the configuration of <see cref="TreeEntity{TEntity, TConfiguration, TNode}"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class TreeEntityConfiguration<TConfiguration, TEntity, TNode> : TreeEntityConfiguration<TConfiguration, TEntity>
        where TConfiguration : TreeEntityConfiguration<TConfiguration, TEntity, TNode> 
        where TEntity : TreeEntity<TEntity, TConfiguration, TNode>
        where TNode : TreeNode, new()
    {
    }
}
