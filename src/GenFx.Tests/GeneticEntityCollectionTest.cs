using System;
using System.Collections.ObjectModel;
using System.Linq;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    ///This is a test class for GenFx.GeneticEntityCollection and is intended
    ///to contain all GenFx.GeneticEntityCollection Unit Tests
    ///</summary>
    public class EntityCollectionTest
    {
        /// <summary>
        /// Tests that the SortByFitness method works correctly.
        /// </summary>
        [Fact]
        public void EntityCollection_SortByFitness()
        {
            ObservableCollection<GeneticEntity> geneticEntities = new ObservableCollection<GeneticEntity>();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity()
            };

            for (int i = 9; i >= 0; i--)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                entity.ScaledFitnessValue = Convert.ToDouble(i);
                geneticEntities.Add(entity);
            }

            GeneticEntity[] sortedEntities = geneticEntities.GetEntitiesSortedByFitness(FitnessType.Scaled, FitnessEvaluationMode.Maximize).ToArray();
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(Convert.ToDouble(i), sortedEntities[i].ScaledFitnessValue);
            }

            sortedEntities = geneticEntities.GetEntitiesSortedByFitness(FitnessType.Scaled, FitnessEvaluationMode.Minimize).ToArray();
            int entityIndex = 0;
            for (int i = 9; i >= 0; i--)
            {
                Assert.Equal(Convert.ToDouble(i), sortedEntities[entityIndex].ScaledFitnessValue);
                entityIndex++;
            }
        }
    }
}
