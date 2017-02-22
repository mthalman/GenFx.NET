using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a tree that is a type of <see cref="GeneticEntity"/>.
    /// </summary>
    /// <remarks>
    /// The <b>TreeEntity</b> is used to represent genetic entities that require a tree structure, such as
    /// expression trees.  Trees generally do not have a fixed size and can grow infinitely.
    /// </remarks>
    [DataContract]
    public abstract class TreeEntityBase : GeneticEntity
    {
        [DataMember]
        private TreeNode rootNode;
        
        /// <summary>
        /// Gets the <see cref="TreeNode"/> representing the root of the tree.
        /// </summary>
        /// <value>The <see cref="TreeNode"/> representing the root of the tree.</value>
        public TreeNode RootNode
        {
            get { return this.rootNode; }
        }

        /// <summary>
        /// Copies the state from this object> to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="GeneticEntity"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(GeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            TreeEntityBase treeEntity = (TreeEntityBase)entity;

            treeEntity.rootNode = this.rootNode.Clone(treeEntity, null);

            base.CopyTo(entity);
        }

        /// <summary>
        /// Sets the <see cref="RootNode"/> property to <paramref name="node"/>.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to be set as the root node of this tree.</param>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public void SetRootNode(TreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.rootNode = node;
            this.rootNode.ParentNode = null;
            node.Tree = this;
            TreeHelper.SetTreeForChildNodes(node);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings:
        ///  * Less than zero: This object is less than <paramref name="other"/>.
        ///  * Zero: This object is equal to <paramref name="other"/>.
        ///  * Greater than zero: This object is greater than <paramref name="other"/>.
        ///  </returns>
        public override int CompareTo(GeneticEntity other)
        {
            if (other == null)
            {
                return 1;
            }

            TreeEntityBase treeEntityBase = other as TreeEntityBase;
            if (treeEntityBase == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    Resources.ErrorMsg_ObjectIsWrongType, typeof(TreeEntityBase)), nameof(other));
            }

            List<TreeNode> thisTree = this.GetPrefixTree().ToList();
            List<TreeNode> otherTree = treeEntityBase.GetPrefixTree().ToList();

            return ComparisonHelper.CompareLists(
                (IList)thisTree.Select(n => n.Value).ToList(),
                (IList)otherTree.Select(n => n.Value).ToList());
        }
    }
}
