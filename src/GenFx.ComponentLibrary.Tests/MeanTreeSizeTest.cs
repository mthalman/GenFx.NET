using GenFx.ComponentLibrary.Metrics;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MeanTreeSize"/> class.
    /// </summary>
    [TestClass]
    public class MeanTreeSizeTest
    {
        /// <summary>
        /// Tests that the GetResultValue method works correctly.
        /// </summary>
        [TestMethod]
        public void MeanTreeSize_GetResultValue()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            algorithm.Metrics.Add(new MeanTreeSize());

            MeanTreeSize target = new MeanTreeSize();
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
        [TestMethod]
        public void MeanTreeSize_GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            algorithm.Metrics.Add(new MeanTreeSize());

            MeanTreeSize target = new MeanTreeSize();
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
