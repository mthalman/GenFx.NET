using System;
using System.Collections.Generic;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    ///This is a test class for GenFx.SelectionOperator and is intended
    ///to contain all GenFx.SelectionOperator Unit Tests
    ///</summary>
    public class SelectionOperatorTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void SelectionOperator_Ctor_NullAlgorithm()
        {
            MockSelectionOperator op = new MockSelectionOperator();
            Assert.Throws<ArgumentNullException>(() => op.Initialize(null));
        }

        /// <summary>
        /// Tests that the Select method works correctly.
        /// </summary>
        [Fact]
        public void SelectionOperator_Select()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            MockSelectionOperator op = new MockSelectionOperator();
            op.Initialize(algorithm);
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            GeneticEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            GeneticEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            IList<GeneticEntity> selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity1, selectedEntities[0]);
            Assert.Equal(1, op.DoSelectCallCount);
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [Fact]
        public void SelectionOperator_Select_EmptyPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            MockSelectionOperator op = new MockSelectionOperator();
            op.Initialize(algorithm);
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            Assert.Throws<ArgumentException>(() => op.SelectEntities(1, population));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [Fact]
        public void SelectionOperator_Select_NullPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            MockSelectionOperator op = new MockSelectionOperator();
            op.Initialize(algorithm);
            Assert.Throws<ArgumentNullException>(() => op.SelectEntities(1, null));
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void SelectionOperator_Serialization()
        {
            MockSelectionOperator op = new MockSelectionOperator
            {
                SelectionBasedOnFitnessType = FitnessType.Raw
            };

            MockSelectionOperator result = (MockSelectionOperator)SerializationHelper.TestSerialization(op, new Type[0]);

            Assert.Equal(op.SelectionBasedOnFitnessType, result.SelectionBasedOnFitnessType);
        }

        /// <summary>
        /// Tests that an exception is thrown when the result of <see cref="SelectionOperator.SelectEntitiesFromPopulation"/>
        /// is null while executing <see cref="SelectionOperator.SelectEntities"/>.
        /// </summary>
        [Fact]
        public void SelectionOperator_SelectEntities_NullSelectionResult()
        {
            MockSelectionOperator2 op = new MockSelectionOperator2();
            MockPopulation population = new MockPopulation();
            population.Entities.Add(new MockEntity());
            Assert.Throws<InvalidOperationException>(() => op.SelectEntities(0, population));
        }

        private GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            };
            return algorithm;
        }
    }
}
