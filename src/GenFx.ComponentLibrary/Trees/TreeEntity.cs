using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a generic tree that is a type of <see cref="GeneticEntity"/>.
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class TreeEntity<TNode> : TreeEntityBase
        where TNode : TreeNode, new()
    {
        /// <summary>
        /// Gets the <see cref="TreeNode"/> representing the root of the tree.
        /// </summary>
        /// <value>The <see cref="TreeNode"/> representing the root of the tree.</value>
        public new TNode RootNode
        {
            get { return (TNode)base.RootNode; }
        }
    }
}
