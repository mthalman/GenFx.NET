using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a tree whose nodes can have an arbitrary number of 
    /// children.
    /// </summary>
    /// <typeparam name="T">The type of nodes in the tree.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NAry")]
    public abstract class NAryTreeEntity<T> : TreeEntity<T>
        where T : TreeNode, new()
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NAryTreeEntity{T}"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="NAryTreeEntity{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected NAryTreeEntity(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="NAryTreeEntity{T}"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NAry")]
    [Component(typeof(NAryTreeEntity<>))]
    public abstract class NAryTreeEntityConfiguration : TreeEntityConfiguration
    {
    }
}
