using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.Components.Trees
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
    [DataContract]
    [RequiredGeneticEntity(typeof(TreeEntityBase))]
    public class SingleNodeTreeCrossoverOperator : CrossoverOperator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public SingleNodeTreeCrossoverOperator()
            : base(2)
        {
        }

        /// <summary>
        /// When overriden in a derived class, generates a crossover based on the parent entities.
        /// </summary>
        /// <param name="parents">The <see cref="GeneticEntity"/> objects to be operated upon.</param>
        /// <returns>
        /// Collection of the <see cref="GeneticEntity"/> objects resulting from the crossover.
        /// </returns>
        protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
        {
            if (parents == null)
            {
                throw new ArgumentNullException(nameof(parents));
            }

            TreeEntityBase tree1 = (TreeEntityBase)parents[0];
            TreeEntityBase tree2 = (TreeEntityBase)parents[1];

            int crossoverLocation1 = RandomNumberService.Instance.GetRandomValue(tree1.GetSize());
            int crossoverLocation2 = RandomNumberService.Instance.GetRandomValue(tree2.GetSize());

            TreeNode swapNode1 = GetSwapNode(tree1, crossoverLocation1);
            TreeNode swapNode2 = GetSwapNode(tree2, crossoverLocation2);

            TreeHelper.Swap(swapNode1, swapNode2);

            List<GeneticEntity> geneticEntities = new List<GeneticEntity>
            {
                tree1,
                tree2
            };

            return geneticEntities;
        }

        /// <summary>
        /// Returns the node at the specified prefix position.
        /// </summary>
        private static TreeNode GetSwapNode(TreeEntityBase tree, int nodePosition)
        {
            Debug.Assert(nodePosition < tree.GetSize(), "nodePosition must be less than the size of the tree.");
            return tree.GetPrefixTree().Where((node, index) => index == nodePosition).FirstOrDefault();
        }
    }
}
