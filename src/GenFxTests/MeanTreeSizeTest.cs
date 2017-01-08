using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Statistics;
using GenFx.ComponentLibrary.Trees;
using GenFx.Contracts;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
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
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Entity = new TestTreeEntityConfiguration(),
                Population = new SimplePopulationFactoryConfig(),
            };
            config.Statistics.Add(new MeanTreeSizeStatisticFactoryConfig());

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            MeanTreeSizeStatistic target = new MeanTreeSizeStatistic(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);

            ITreeEntity entity = new TestTreeEntity(algorithm);
            entity.SetRootNode(new TreeNode());
            entity.RootNode.ChildNodes.Add(new TreeNode());
            entity.RootNode.ChildNodes.Add(new TreeNode());
            entity.RootNode.ChildNodes[0].ChildNodes.Add(new TreeNode());
            population.Entities.Add(entity);

            entity = new TestTreeEntity(algorithm);
            entity.SetRootNode(new TreeNode());
            population.Entities.Add(entity);

            entity = new TestTreeEntity(algorithm);
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
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Entity = new TestTreeEntityConfiguration(),
                Population = new SimplePopulationFactoryConfig(),
            };
            config.Statistics.Add(new MeanTreeSizeStatisticFactoryConfig());

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            MeanTreeSizeStatistic target = new MeanTreeSizeStatistic(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }

        private class TestTreeEntity : TreeEntity<TestTreeEntity, TestTreeEntityConfiguration, TreeNode>
        {
            public TestTreeEntity(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            protected override void InitializeCore()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class TestTreeEntityConfiguration : TreeEntityFactoryConfig<TestTreeEntityConfiguration, TestTreeEntity, TreeNode>
        {
        }
    }
}
