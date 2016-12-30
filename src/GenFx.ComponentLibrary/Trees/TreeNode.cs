using System;
using GenFx.ComponentLibrary.Properties;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents the node of a tree represented by a <see cref="ITreeEntity"/>.
    /// </summary>
    /// <remarks>
    /// The <b>TreeNode</b> objects of a tree contain the data making up the representation of that tree.
    /// For example, the data in the nodes of an expression tree represent the functions and 
    /// terminators of the expression language.
    /// </remarks>
    public class TreeNode
    {
        private TreeNodeCollection childNodes;
        private TreeNode parentNode;
        private ITreeEntity tree;
        private object nodeValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode"/> class.
        /// </summary>
        /// <param name="fixedSizeCount">Number of sub-node this node contains.</param>
        public TreeNode(int fixedSizeCount)
        {
            this.childNodes = new TreeNodeCollection(fixedSizeCount);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode"/> class.
        /// </summary>
        public TreeNode()
        {
            this.childNodes = new TreeNodeCollection();
        }

        /// <summary>
        /// Gets the parent of this node.
        /// </summary>
        /// <value>The parent of this node.  If the node is a root node, the parent is null.</value>
        public TreeNode ParentNode
        {
            get { return this.parentNode; }
            internal set { this.parentNode = value; }
        }

        /// <summary>
        /// Gets or sets the data value contained by this node.
        /// </summary>
        /// <value>The data value contained by this node.</value>
        public object Value
        {
            get { return this.nodeValue; }
            set { this.nodeValue = value; }
        }

        /// <summary>
        /// Gets the child nodes contained by this node.
        /// </summary>
        /// <value><see cref="TreeNodeCollection"/> containing the child nodes of this node.</value>
        public TreeNodeCollection ChildNodes
        {
            get { return this.childNodes; }
        }

        /// <summary>
        /// Gets the <see cref="ITreeEntity"/> containing this node.
        /// </summary>
        /// <value>The <see cref="ITreeEntity"/> containing this node.</value>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public ITreeEntity Tree
        {
            get { return this.tree; }
            internal set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.tree = value;
            }
        }

        /// <summary>
        /// Adds the <paramref name="node"/> to the end of the list of child nodes.
        /// </summary>
        /// <param name="node">The <see cref="TreeNode"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        public void AppendChild(TreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.AppendChildCore(node);
        }

        /// <summary>
        /// Adds the <paramref name="node"/> to the end of the list of child nodes.
        /// </summary>
        /// <param name="node">The <see cref="TreeNode"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        protected virtual void AppendChildCore(TreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.InsertChild(this.childNodes.Count, node);
        }

        /// <summary>
        /// Inserts the <paramref name="node"/> in the list of child nodes at the position specified.
        /// </summary>
        /// <param name="index">Position in the list of child nodes where the <paramref name="node"/> is to be inserted.</param>
        /// <param name="node"><see cref="TreeNode"/> to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the number of child nodes.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        public void InsertChild(int index, TreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.InsertChildCore(index, node);
        }

        /// <summary>
        /// Inserts the <paramref name="node"/> in the list of child nodes at the position specified.
        /// </summary>
        /// <param name="index">Position in the list of child nodes where the <paramref name="node"/> is to be inserted.</param>
        /// <param name="node"><see cref="TreeNode"/> to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the number of child nodes.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Tree has not been set.</exception>
        protected virtual void InsertChildCore(int index, TreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (this.tree == null)
            {
                throw new InvalidOperationException(
                  StringUtil.GetFormattedString(LibResources.ErrorMsg_InsertChildNodeWithoutTree));
            }

            this.childNodes.Insert(index, node);
            node.ParentNode = this;
            node.Tree = this.tree;
            TreeHelper.SetTreeForChildNodes(node);
        }

        /// <summary>
        /// Returns a clone of this node.
        /// </summary>
        /// <param name="newTree"><see cref="ITreeEntity"/> to which the cloned node should be assigned.</param>
        /// <param name="newParentNode"><see cref="TreeNode"/> to be assigned as the parent node of the cloned node.</param>
        /// <returns>A clone of this node.</returns>
        /// <remarks>
        /// <b>Notes to implementers:</b> When this method is overriden, it is suggested that the
        /// <see cref="TreeNode.CopyTo(TreeNode, ITreeEntity, TreeNode)"/> method is also overriden.  
        /// In that case, the suggested implementation of this method is the following:
        /// <code>
        /// <![CDATA[
        /// public override object Clone(TreeEntity newTree, TreeNode newParentNode)
        /// {
        ///     MyTreeNode node = new MyTreeNode();
        ///     this.CopyTo(node, newTree, newParentNode);
        ///     return node;
        /// }
        /// ]]>
        /// </code>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="newTree"/> is null.</exception>
        public virtual TreeNode Clone(ITreeEntity newTree, TreeNode newParentNode)
        {
            if (newTree == null)
            {
                throw new ArgumentNullException(nameof(newTree));
            }

            TreeNode node = new TreeNode();
            this.CopyTo(node, newTree, newParentNode);
            return node;
        }

        /// <summary>
        /// Copies the state from this <see cref="TreeNode"/> to <paramref name="node"/>.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to which state is to be copied.</param>
        /// <param name="newTree"><see cref="ITreeEntity"/> to which the <paramref name="node"/> should be assigned.</param>
        /// <param name="newParentNode"><see cref="TreeNode"/> to be assigned as the parent node of the <paramref name="node"/>.</param>
        /// <remarks>
        /// <para>
        /// The default implementation of this method is to copy the state of <see cref="TreeNode"/>
        /// to the <see cref="TreeNode"/> passed in.
        /// </para>
        /// <para>
        /// <b>Notes to inheritors:</b> When overriding this method, it is necessary to call the
        /// <b>CopyTo</b> method of the base class.  This method should be used in conjunction with
        /// the <see cref="TreeNode.Clone(ITreeEntity, TreeNode)"/> method.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="newTree"/> is null.</exception>
        public virtual void CopyTo(TreeNode node, ITreeEntity newTree, TreeNode newParentNode)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (newTree == null)
            {
                throw new ArgumentNullException(nameof(newTree));
            }

            for (int i = 0; i < this.childNodes.Count; i++)
            {
                node.ChildNodes.Add(this.childNodes[i].Clone(newTree, node));
            }

            node.ParentNode = newParentNode;
            node.Tree = newTree;
            node.Value = this.nodeValue;
        }
    }

    /// <summary>
    /// Represents a generic node in a <see cref="ITreeEntity"/>.
    /// </summary>
    /// <typeparam name="T">Type of value contained by the node.</typeparam>
    public class TreeNode<T> : TreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}"/> class.
        /// </summary>
        /// <param name="fixedSizeCount">Number of sub-node this node contains.</param>
        public TreeNode(int fixedSizeCount)
            : base(fixedSizeCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}"/> class.
        /// </summary>
        public TreeNode()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the data value contained by this node.
        /// </summary>
        /// <value>The data value contained by this node.</value>
        public new T Value
        {
            get { return (T)base.Value; }
            set { base.Value = value; }
        }

        /// <summary>
        /// Returns a clone of this node.
        /// </summary>
        /// <param name="newTree"><see cref="ITreeEntity"/> to which the cloned node should be assigned.</param>
        /// <param name="newParentNode"><see cref="TreeNode"/> to be assigned as the parent node of the cloned node.</param>
        /// <returns>A clone of this node.</returns>
        /// <remarks>
        /// <b>Notes to implementers:</b> When this method is overriden, it is suggested that the
        /// <see cref="TreeNode.CopyTo(TreeNode, ITreeEntity, TreeNode)"/> method is also overriden.  
        /// In that case, the suggested implementation of this method is the following:
        /// <code>
        /// <![CDATA[
        /// public override object Clone(TreeEntity newTree, TreeNode newParentNode)
        /// {
        ///     MyTreeNode node = new MyTreeNode();
        ///     this.CopyTo(node, newTree, newParentNode);
        ///     return node;
        /// }
        /// ]]>
        /// </code>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="newTree"/> is null.</exception>
        public override TreeNode Clone(ITreeEntity newTree, TreeNode newParentNode)
        {
            if (newTree == null)
            {
                throw new ArgumentNullException(nameof(newTree));
            }

            TreeNode<T> node = new TreeNode<T>();
            this.CopyTo(node, newTree, newParentNode);
            return node;
        }
    }
}
