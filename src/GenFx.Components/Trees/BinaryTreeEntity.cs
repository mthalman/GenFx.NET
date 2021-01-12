using System;
using System.Runtime.Serialization;

namespace GenFx.Components.Trees
{
    /// <summary>
    /// Provides the abstract base class for a binary tree.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the nodes of the tree.</typeparam>
    [DataContract]
    public abstract class BinaryTreeEntity<TValue> : TreeEntity<BinaryTreeNode<TValue>>
        where TValue : IComparable
    {
    }
}
