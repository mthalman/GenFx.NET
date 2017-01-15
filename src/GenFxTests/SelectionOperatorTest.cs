using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
using GenFxTests.Helpers;
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
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void SelectionOperator_Ctor_NullAlgorithm()
        {
            MockSelectionOperator op = new MockSelectionOperator();
            AssertEx.Throws<ArgumentNullException>(() => op.Initialize(null));
        }

        /// <summary>
        /// Tests that the Select method works correctly.
        /// </summary>
        [TestMethod]
        public void SelectionOperator_Select()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            MockSelectionOperator op = new MockSelectionOperator();
            op.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            IGeneticEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            IGeneticEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            IGeneticEntity selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");
            Assert.AreEqual(1, op.DoSelectCallCount, "Selection not called correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod]
        public void SelectionOperator_Select_EmptyPopulation()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            MockSelectionOperator op = new MockSelectionOperator();
            op.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            AssertEx.Throws<ArgumentException>(() => op.SelectEntity(population));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod]
        public void SelectionOperator_Select_NullPopulation()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            MockSelectionOperator op = new MockSelectionOperator();
            op.Initialize(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => op.SelectEntity(null));
        }

        private IGeneticAlgorithm GetAlgorithm()
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            };
            return algorithm;
        }
    }
}
