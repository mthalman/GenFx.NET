using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides a <see cref="TreeEntityBase"/> with single-node tree crossover support.
    /// </summary>
    /// <remarks>
    /// Single-node tree crossover chooses a node position in two trees and swaps those nodes, including their children.
    /// For example, if two <see cref="TreeEntityBase"/> objects represented as "(A OR B) AND C" and
    /// "B OR C" were to be crossed over at the "OR" node in the first tree and the "C" node in the second tree,
    /// the resulting offspring would be "C AND C" and "B OR (A OR B)".
    /// </remarks>
    [RequiredEntity(typeof(TreeEntityBase))]
    public class SingleNodeTreeCrossoverOperator : CrossoverOperator
    {
        /// <summary>
        /// Executes a single-node crossover between two <see cref="TreeEntityBase"/> objects.
        /// </summary>
        /// <param name="entity1"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the <see cref="TreeEntityBase"/> objects resulting from the crossover.  If no
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

            TreeEntityBase tree1 = (TreeEntityBase)entity1;
            TreeEntityBase tree2 = (TreeEntityBase)entity2;

            int crossoverLocation1 = RandomNumberService.Instance.GetRandomValue(tree1.GetSize());
            int crossoverLocation2 = RandomNumberService.Instance.GetRandomValue(tree2.GetSize());

            TreeNode swapNode1 = GetSwapNode(tree1, crossoverLocation1);
            TreeNode swapNode2 = GetSwapNode(tree2, crossoverLocation2);

            TreeHelper.Swap(swapNode1, swapNode2);

            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
            geneticEntities.Add(entity1);
            geneticEntities.Add(entity2);

            return geneticEntities;
        }

        /// <summary>
        /// Returns the node at the specified prefix position.
        /// </summary>
        private static TreeNode GetSwapNode(TreeEntityBase tree, int nodePosition)
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
}
