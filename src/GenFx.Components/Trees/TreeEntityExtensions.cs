using System;
using System.Collections.Generic;

namespace GenFx.Components.Trees
{
    /// <summary>
    /// Contains extension methods for <see cref="TreeEntityBase"/>.
    /// </summary>
    public static class TreeEntityExtensions
    {
        /// <summary>
        /// Returns the number of nodes contained in the tree.
        /// </summary>
        /// <returns>The number of nodes contained in the tree.</returns>
        public static int GetSize(this TreeEntityBase treeEntity)
        {
            if (treeEntity is null)
            {
                throw new ArgumentNullException(nameof(treeEntity));
            }

            return TreeEntityExtensions.GetSubtreeSize(treeEntity.RootNode);
        }

        /// <summary>
        /// Returns the number of nodes contained in the subtree of the given <paramref name="node"/>.
        /// </summary>
        private static int GetSubtreeSize(TreeNode? node)
        {
            if (node is null)
            {
                return 0;
            }

            int sum = 1; // Includes the node passed in.

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                sum += TreeEntityExtensions.GetSubtreeSize(node.ChildNodes[i]);
            }

            return sum;
        }

        /// <summary>
        /// Returns an enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.
        /// </summary>
        /// <param name="treeEntity">The tree entity from which to get the nodes.</param>
        /// <returns>
        /// An enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.
        /// </returns>
        public static IEnumerable<TreeNode> GetPrefixTree(this TreeEntityBase treeEntity)
        {
            if (treeEntity is null)
            {
                throw new ArgumentNullException(nameof(treeEntity));
            }

            return TreeEntityExtensions.GetPrefixTree(treeEntity.RootNode);
        }

        /// <summary>
        /// Returns an enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to start at.</param>
        /// <returns>
        /// An enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.
        /// </returns>
        private static IEnumerable<TreeNode> GetPrefixTree(TreeNode? node)
        {
            if (node != null)
            {
                yield return node;
                foreach (TreeNode childNode in node.ChildNodes)
                {
                    foreach (TreeNode subChildNode in TreeEntityExtensions.GetPrefixTree(childNode))
                    {
                        yield return subChildNode;
                    }
                }
            }
        }

        /// <summary>
        /// Returns an enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in postfix order.
        /// </summary>
        /// <param name="treeEntity">The tree entity from which to get the nodes.</param>
        /// <returns>An enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.</returns>
        public static IEnumerable<TreeNode> GetPostfixTree(this TreeEntityBase treeEntity)
        {
            if (treeEntity is null)
            {
                throw new ArgumentNullException(nameof(treeEntity));
            }

            return TreeEntityExtensions.GetPostfixTree(treeEntity.RootNode);
        }

        /// <summary>
        /// Returns an enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in postfix order.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to start at.</param>
        /// <returns>An enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.</returns>
        private static IEnumerable<TreeNode> GetPostfixTree(TreeNode? node)
        {
            if (node != null)
            {
                foreach (TreeNode childNode in node.ChildNodes)
                {
                    foreach (TreeNode subChildNode in TreeEntityExtensions.GetPostfixTree(childNode))
                    {
                        yield return subChildNode;
                    }
                }
                yield return node;
            }
        }
    }
}
