using GenFx.ComponentLibrary.Metrics;
using GenFx.ComponentLibrary.Populations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BestMaximumFitnessEntity"/> class.
    ///</summary>
    [TestClass]
    public class BestMaximumFitnessEntityTest
    {
        /// <summary>
        /// Tests that an exception will be thrown when a null population is passed to <see cref="BestMaximumFitnessEntity.GetResultValue"/>.
        /// </summary>
        [TestMethod]
        public void BestMaximumFitnessEntityGetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Metrics.Add(new BestMaximumFitnessEntity());
            BestMaximumFitnessEntity target = new BestMaximumFitnessEntity();
            target.Initialize(algorithm);

            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }

        /// <summary>
        /// Tests that the correct value is returned from <see cref="BestMaximumFitnessEntity.GetResultValue"/>.
        /// </summary>
        [TestMethod]
        public void BestMaximumFitnessEntity_GetResultValue()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            algorithm.Metrics.Add(new BestMaximumFitnessEntity());

            BestMaximumFitnessEntity target = new BestMaximumFitnessEntity();
            target.Initialize(algorithm);

            SimplePopulation population1 = new SimplePopulation()
            {
                Index = 0
            };
            population1.Initialize(algorithm);

            VerifyGetResultValue(2, target, population1, algorithm, "20");
            VerifyGetResultValue(1, target, population1, algorithm, "20");
            VerifyGetResultValue(3, target, population1, algorithm, "30");

            SimplePopulation population2 = new SimplePopulation()
            {
                Index = 1
            };
            population2.Initialize(algorithm);

            VerifyGetResultValue(7, target, population2, algorithm, "70");
            VerifyGetResultValue(1, target, population1, algorithm, "30");
            VerifyGetResultValue(4, target, population2, algorithm, "70");
        }

        /// <summary>
        /// Tests that the component can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void BestMaximumFitnessEntity_Serialization()
        {
            BestMaximumFitnessEntity metric = new BestMaximumFitnessEntity();
            PrivateObject privObj = new PrivateObject(metric);
            Dictionary<int, GeneticEntity> bestEntities = (Dictionary<int, GeneticEntity>)privObj.GetField("bestEntities");
            bestEntities.Add(10, new MockEntity());

            BestMaximumFitnessEntity result = (BestMaximumFitnessEntity)SerializationHelper.TestSerialization(metric, new Type[] { typeof(MockEntity) });

            PrivateObject resultPrivObj = new PrivateObject(result);
            Dictionary<int, GeneticEntity> resultBestEntities = (Dictionary<int, GeneticEntity>)resultPrivObj.GetField("bestEntities");
            Assert.IsInstanceOfType(resultBestEntities[10], typeof(MockEntity));
        }
        
        private static void VerifyGetResultValue(int multiplier, BestMaximumFitnessEntity metric, SimplePopulation population, GeneticAlgorithm algorithm, string expectedReturnVal)
        {
            for (int i = 0; i < 5; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                entity.ScaledFitnessValue = i * multiplier;
                entity.Identifier = entity.ScaledFitnessValue.ToString();
                population.Entities.Add(entity);
            }

            for (int i = 10; i >= 5; i--)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                entity.ScaledFitnessValue = i * multiplier;
                entity.Identifier = entity.ScaledFitnessValue.ToString();
                population.Entities.Add(entity);
            }

            object representation = metric.GetResultValue(population);

            Assert.AreEqual(expectedReturnVal, representation.ToString());
        }
    }
}
