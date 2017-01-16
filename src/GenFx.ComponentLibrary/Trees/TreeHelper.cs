using System;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Helper class used for tree-related functionality.
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// Swaps the position of the two nodes within their respective trees.
        /// </summary>
        /// <remarks><paramref name="node1"/> will be removed from its tree and be placed in the
        /// position that <paramref name="node2"/> existed in its tree; and vice versa.</remarks>
        /// <param name="node1"><see cref="TreeNode"/> to be swapped with <paramref name="node2"/>.</param>
        /// <param name="node2"><see cref="TreeNode"/> to be swapped with <paramref name="node1"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="node1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="node2"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="node1"/> is not contained by this tree.</exception>
        public static void Swap(TreeNode node1, TreeNode node2)
        {
            if (node1 == null)
            {
                throw new ArgumentNullException(nameof(node1));
            }
            if (node2 == null)
            {
                throw new ArgumentNullException(nameof(node2));
            }

            TreeNode node2ParentNode = node2.ParentNode;
            TreeEntityBase node2Tree = node2.Tree;
            TreeNode node1ParentNode = node1.ParentNode;
            TreeEntityBase node1Tree = node1.Tree;

            TreeHelper.MoveNodeToTree(node1, node2Tree, node2, node2ParentNode);
            TreeHelper.MoveNodeToTree(node2, node1Tree, node1, node1ParentNode);
        }

        /// <summary>
        /// Moves <paramref name="movingNode"/> with all of its children to the location of <paramref name="locationNode"/>.
        /// </summary>
        /// <param name="movingNode"><see cref="TreeNode"/> to be moved.</param>
        /// <param name="locationNodeTree"><see cref="TreeEntityBase"/> containing the <paramref name="locationNode"/>.</param>
        /// <param name="locationNode"><see cref="TreeNode"/> where <paramref name="movingNode"/> should be moved to.</param>
        /// <param name="locationParentNode"><see cref="TreeNode"/> of the parent of <paramref name="locationNode"/>.</param>
        public static void MoveNodeToTree(TreeNode movingNode, TreeEntityBase locationNodeTree, TreeNode locationNode, TreeNode locationParentNode)
        {
            if (locationNodeTree == null)
            {
                throw new ArgumentNullException(nameof(locationNodeTree));
            }

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
        public static void SetTreeForChildNodes(TreeNode parentNode)
        {
            if (parentNode == null)
            {
                throw new ArgumentNullException("parentNode");
            }

            for (int i = 0; i < parentNode.ChildNodes.Count; i++)
            {
                TreeNode childNode = parentNode.ChildNodes[i];
                childNode.Tree = parentNode.Tree;
                SetTreeForChildNodes(childNode);
            }
        }
    }
}
