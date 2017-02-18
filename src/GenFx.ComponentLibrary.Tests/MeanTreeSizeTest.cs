using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Statistics;
using GenFx.ComponentLibrary.Trees;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Statistics.MeanTreeSize and is intended
    /// to contain all GenFx.ComponentLibrary.Statistics.MeanTreeSize Unit Tests
    /// </summary>
    [TestClass()]
    public class MeanTreeSizeTest
    {
        /// <summary>
        /// Tests that the GetResultValue method works correctly.
        /// </summary>
        [TestMethod()]
        public void MeanTreeSize_GetResultValue()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            algorithm.Statistics.Add(new MeanTreeSizeStatistic());

            MeanTreeSizeStatistic target = new MeanTreeSizeStatistic();
            target.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);

            TreeEntityBase entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            entity.SetRootNode(new TreeNode());
            entity.RootNode.ChildNodes.Add(new TreeNode());
            entity.RootNode.ChildNodes.Add(new TreeNode());
            entity.RootNode.ChildNodes[0].ChildNodes.Add(new TreeNode());
            population.Entities.Add(entity);

            entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            entity.SetRootNode(new TreeNode());
            population.Entities.Add(entity);

            entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            entity.SetRootNode(new TreeNode());
            entity.RootNode.ChildNodes.Add(new TreeNode());
            population.Entities.Add(entity);

            object result = target.GetResultValue(population);

            Assert.AreEqual(2.33, Math.Round((double)result, 2), "Incorrect result value.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        public void MeanTreeSize_GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            algorithm.Statistics.Add(new MeanTreeSizeStatistic());

            MeanTreeSizeStatistic target = new MeanTreeSizeStatistic();
            target.Initialize(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }

        private class TestTreeEntity : TreeEntity<TreeNode>
        {
            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }
        }
    }
}
