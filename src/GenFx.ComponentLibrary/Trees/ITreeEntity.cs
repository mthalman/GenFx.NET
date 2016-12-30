namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents an entity that is a tree (a connected, undirected graph).
    /// </summary>
    public interface ITreeEntity : IGeneticEntity
    {
        /// <summary>
        /// Gets the <see cref="TreeNode"/> representing the root of the tree.
        /// </summary>
        /// <value>The <see cref="TreeNode"/> representing the root of the tree.</value>
        TreeNode RootNode { get; }

        /// <summary>
        /// Sets the <see cref="RootNode"/> property to <paramref name="node"/>.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to be set as the root node of this tree.</param>
        void SetRootNode(TreeNode node);
    }
}
