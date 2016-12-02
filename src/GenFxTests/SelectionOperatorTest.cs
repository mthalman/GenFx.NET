using GenFx;
using GenFx.ComponentModel;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.SelectionOperator and is intended
    ///to contain all GenFx.SelectionOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class SelectionOperatorTest
    {

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void SelectionOperator_Ctor()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            SelectionOperator op = new MockSelectionOperator(algorithm);
            Assert.AreEqual(FitnessType.Scaled, op.SelectionBasedOnFitnessType, "SelectionBasedOnFitnessType not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SelectionOperator_Ctor_NullAlgorithm()
        {
            SelectionOperator op = new MockSelectionOperator(null);
        }

        /// <summary>
        /// Tests that an exception is thrown when a required config is missing.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectionOperator_Ctor_MissingConfig()
        {
            SelectionOperator op = new MockSelectionOperator(new MockGeneticAlgorithm());
        }

        /// <summary>
        /// Tests that the Select method works correctly.
        /// </summary>
        [TestMethod]
        public void SelectionOperator_Select()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            MockSelectionOperator op = new MockSelectionOperator(algorithm);
            Population population = new Population(algorithm);
            GeneticEntity entity1 = new MockEntity(algorithm);
            GeneticEntity entity2 = new MockEntity(algorithm);
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            GeneticEntity selectedEntity = op.Select(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");
            Assert.AreEqual(1, op.DoSelectCallCount, "Selection not called correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectionOperator_Select_EmptyPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            MockSelectionOperator op = new MockSelectionOperator(algorithm);
            Population population = new Population(algorithm);
            GeneticEntity selectedEntity = op.Select(population);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SelectionOperator_Select_NullPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            MockSelectionOperator op = new MockSelectionOperator(algorithm);
            GeneticEntity selectedEntity = op.Select(null);
        }

        private GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            MockSelectionOperatorConfiguration config = new MockSelectionOperatorConfiguration();
            config.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = config;
            return algorithm;
        }
    }


}
