namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Helper class used for tree-related functionality.
    /// </summary>
    internal static class TreeUtil
    {
        /// <summary>
        /// Moves <paramref name="movingNode"/> with all of its children to the location of <paramref name="locationNode"/>.
        /// </summary>
        /// <param name="movingNode"><see cref="TreeNode"/> to be moved.</param>
        /// <param name="locationNodeTree"><see cref="TreeEntity"/> containing the <paramref name="locationNode"/>.</param>
        /// <param name="locationNode"><see cref="TreeNode"/> where <paramref name="movingNode"/> should be moved to.</param>
        /// <param name="locationParentNode"><see cref="TreeNode"/> of the parent of <paramref name="locationNode"/>.</param>
        internal static void MoveNodeToTree(TreeNode movingNode, TreeEntity locationNodeTree, TreeNode locationNode, TreeNode locationParentNode)
        {
            if (locationParentNode == null)
            {
                locationNodeTree.SetRootNode(movingNode);
            }
            else
            {
                int childIndex = locationParentNode.ChildNodes.IndexOf(locationNode);
                locationParentNode.ChildNodes.Remove(locationNode);
                locationParentNode.InsertChild(childIndex, movingNode);
            }
        }

        /// <summary>
        /// Sets the <see cref="TreeNode.Tree"/> property for the children of <paramref name="parentNode"/>.
        /// </summary>
        /// <param name="parentNode"><see cref="TreeNode"/> whose children should be set.</param>
        internal static void SetTreeForChildNodes(TreeNode parentNode)
        {
            for (int i = 0; i < parentNode.ChildNodes.Count; i++)
            {
                TreeNode childNode = parentNode.ChildNodes[i];
                childNode.Tree = parentNode.Tree;
                SetTreeForChildNodes(childNode);
            }
        }
    }
}
