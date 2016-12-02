using GenFx;
using GenFx.ComponentLibrary.Statistics;
using GenFx.ComponentLibrary.Trees;
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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Entity = new TestTreeEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new MeanTreeSizeStatisticConfiguration());
            MeanTreeSizeStatistic target = new MeanTreeSizeStatistic(algorithm);
            Population population = new Population(algorithm);

            TreeEntity entity = new TestTreeEntity(algorithm);
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void MeanTreeSize_GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new MeanTreeSizeStatisticConfiguration());
            MeanTreeSizeStatistic target = new MeanTreeSizeStatistic(algorithm);
            object result = target.GetResultValue(null);
        }

        private class TestTreeEntity : TreeEntity<TreeNode>
        {
            public TestTreeEntity(GeneticAlgorithm algorithm)
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

            public override GeneticEntity Clone()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [Component(typeof(TestTreeEntity))]
        private class TestTreeEntityConfiguration : TreeEntityConfiguration
        {
        }
    }
}
