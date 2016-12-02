using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides a <see cref="TreeEntity"/> with single-node tree crossover support.
    /// </summary>
    /// <remarks>
    /// Single-node tree crossover chooses a node position in two trees and swaps those nodes, including their children.
    /// For example, if two <see cref="TreeEntity"/> objects represented as "(A OR B) AND C" and
    /// "B OR C" were to be crossed over at the "OR" node in the first tree and the "C" node in the second tree,
    /// the resulting offspring would be "C AND C" and "B OR (A OR B)".
    /// </remarks>
    [RequiredEntity(typeof(TreeEntity))]
    public class SingleNodeTreeCrossoverOperator : CrossoverOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleNodeTreeCrossoverOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="SingleNodeTreeCrossoverOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public SingleNodeTreeCrossoverOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Executes a single-node crossover between two <see cref="TreeEntity"/> objects.
        /// </summary>
        /// <param name="entity1"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the <see cref="TreeEntity"/> objects resulting from the crossover.  If no
        /// crossover occurred, this collection contains the original values of <paramref name="entity1"/>
        /// and <paramref name="entity2"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="entity2"/> is null.</exception>
        protected override IList<GeneticEntity> GenerateCrossover(GeneticEntity entity1, GeneticEntity entity2)
        {
            if (entity1 == null)
            {
                throw new ArgumentNullException(nameof(entity1));
            }

            if (entity2 == null)
            {
                throw new ArgumentNullException(nameof(entity2));
            }

            TreeEntity tree1 = (TreeEntity)entity1;
            TreeEntity tree2 = (TreeEntity)entity2;

            int crossoverLocation1 = RandomHelper.Instance.GetRandomValue(tree1.GetSize());
            int crossoverLocation2 = RandomHelper.Instance.GetRandomValue(tree2.GetSize());

            TreeNode swapNode1 = SingleNodeTreeCrossoverOperator.GetSwapNode(tree1, crossoverLocation1);
            TreeNode swapNode2 = SingleNodeTreeCrossoverOperator.GetSwapNode(tree2, crossoverLocation2);

            tree1.Swap(swapNode1, swapNode2);

            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
            geneticEntities.Add(entity1);
            geneticEntities.Add(entity2);

            return geneticEntities;
        }

        /// <summary>
        /// Returns the node at the specified prefix position.
        /// </summary>
        private static TreeNode GetSwapNode(TreeEntity tree, int nodePosition)
        {
            Debug.Assert(nodePosition < tree.GetSize(), "nodePosition must be less than the size of the tree.");
            int nodeIndex = 0;
            foreach (TreeNode node in tree.GetPrefixTree())
            {
                if (nodeIndex == nodePosition)
                {
                    return node;
                }
                nodeIndex++;
            }

            return null;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="SingleNodeTreeCrossoverOperator"/>.
    /// </summary>
    [Component(typeof(SingleNodeTreeCrossoverOperator))]
    public class SingleNodeTreeCrossoverOperatorConfiguration : CrossoverOperatorConfiguration
    {
    }
}
