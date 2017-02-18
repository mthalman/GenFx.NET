using GenFx;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="GeneticEntityCollectionExtensions"/> class.
    /// </summary>
    [TestClass]
    public class GeneticEntityCollectionExtensionsTest
    {
        /// <summary>
        /// Tests that <see cref="GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness"/> method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticEntityCollectionExtensions_GetEntitiesSortedByFitness_SortRawMaximize()
        {
            TestEntity testEntity1 = new TestEntity { TestFitnessValue = 1 };
            TestEntity testEntity2 = new TestEntity { TestFitnessValue = 3 };
            TestEntity testEntity3 = new TestEntity { TestFitnessValue = 2 };

            List<GeneticEntity> entities = new List<GeneticEntity>
            {
                testEntity1,
                testEntity2,
                testEntity3,
            };

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new TestFitnessEvalutor()
            };

            foreach (GeneticEntity entity in entities)
            {
                entity.Initialize(algorithm);
                await entity.EvaluateFitnessAsync();
            }

            List<GeneticEntity> results = GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness(entities, FitnessType.Raw, FitnessEvaluationMode.Maximize).ToList();

            Assert.AreEqual(3, results.Count);
            Assert.AreSame(testEntity1, results[0]);
            Assert.AreSame(testEntity3, results[1]);
            Assert.AreSame(testEntity2, results[2]);
        }

        /// <summary>
        /// Tests that <see cref="GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness"/> method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticEntityCollectionExtensions_GetEntitiesSortedByFitness_SortRawMinimize()
        {
            TestEntity testEntity1 = new TestEntity { TestFitnessValue = 1 };
            TestEntity testEntity2 = new TestEntity { TestFitnessValue = 3 };
            TestEntity testEntity3 = new TestEntity { TestFitnessValue = 2 };

            List<GeneticEntity> entities = new List<GeneticEntity>
            {
                testEntity1,
                testEntity2,
                testEntity3,
            };

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new TestFitnessEvalutor()
            };

            foreach (GeneticEntity entity in entities)
            {
                entity.Initialize(algorithm);
                await entity.EvaluateFitnessAsync();
            }

            List<GeneticEntity> results = GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness(entities, FitnessType.Raw, FitnessEvaluationMode.Minimize).ToList();

            Assert.AreEqual(3, results.Count);
            Assert.AreSame(testEntity2, results[0]);
            Assert.AreSame(testEntity3, results[1]);
            Assert.AreSame(testEntity1, results[2]);
        }

        /// <summary>
        /// Tests that <see cref="GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness"/> method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticEntityCollectionExtensions_GetEntitiesSortedByFitness_SortScaledMaximize()
        {
            TestEntity testEntity1 = new TestEntity { TestFitnessValue = 1 };
            TestEntity testEntity2 = new TestEntity { TestFitnessValue = 3 };
            TestEntity testEntity3 = new TestEntity { TestFitnessValue = 2 };

            List<GeneticEntity> entities = new List<GeneticEntity>
            {
                testEntity1,
                testEntity2,
                testEntity3,
            };

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new TestFitnessEvalutor()
            };

            foreach (GeneticEntity entity in entities)
            {
                entity.Initialize(algorithm);
                await entity.EvaluateFitnessAsync();
            }

            testEntity1.ScaledFitnessValue = 3;
            testEntity2.ScaledFitnessValue = 1;
            testEntity3.ScaledFitnessValue = 2;

            List<GeneticEntity> results = GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness(entities, FitnessType.Scaled, FitnessEvaluationMode.Maximize).ToList();

            Assert.AreEqual(3, results.Count);
            Assert.AreSame(testEntity2, results[0]);
            Assert.AreSame(testEntity3, results[1]);
            Assert.AreSame(testEntity1, results[2]);
        }

        /// <summary>
        /// Tests that <see cref="GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness"/> method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticEntityCollectionExtensions_GetEntitiesSortedByFitness_SortScaledMinimize()
        {
            TestEntity testEntity1 = new TestEntity { TestFitnessValue = 1 };
            TestEntity testEntity2 = new TestEntity { TestFitnessValue = 3 };
            TestEntity testEntity3 = new TestEntity { TestFitnessValue = 2 };

            List<GeneticEntity> entities = new List<GeneticEntity>
            {
                testEntity1,
                testEntity2,
                testEntity3,
            };

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new TestFitnessEvalutor()
            };

            foreach (GeneticEntity entity in entities)
            {
                entity.Initialize(algorithm);
                await entity.EvaluateFitnessAsync();
            }

            testEntity1.ScaledFitnessValue = 3;
            testEntity2.ScaledFitnessValue = 1;
            testEntity3.ScaledFitnessValue = 2;

            List<GeneticEntity> results = GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness(entities, FitnessType.Scaled, FitnessEvaluationMode.Minimize).ToList();

            Assert.AreEqual(3, results.Count);
            Assert.AreSame(testEntity1, results[0]);
            Assert.AreSame(testEntity3, results[1]);
            Assert.AreSame(testEntity2, results[2]);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null collection to <see cref="GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness"/>.
        /// </summary>
        [TestMethod]
        public void GeneticEntityCollectionExtensions_GetEntitiesSortedByFitness_NullCollection()
        {
            AssertEx.Throws<ArgumentNullException>(() => GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness(null, FitnessType.Raw, FitnessEvaluationMode.Maximize));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid fitness type to <see cref="GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness"/>.
        /// </summary>
        [TestMethod]
        public void GeneticEntityCollectionExtensions_GetEntitiesSortedByFitness_InvalidFitnessType()
        {
            AssertEx.Throws<ArgumentException>(() => GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness(new GeneticEntity[0], (FitnessType)5, FitnessEvaluationMode.Maximize));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid fitness evaluation mode to <see cref="GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness"/>.
        /// </summary>
        [TestMethod]
        public void GeneticEntityCollectionExtensions_GetEntitiesSortedByFitness_InvalidFitnessEvaluationMode()
        {
            AssertEx.Throws<ArgumentException>(() => GeneticEntityCollectionExtensions.GetEntitiesSortedByFitness(new GeneticEntity[0], FitnessType.Raw, (FitnessEvaluationMode)5));
        }

        private class TestEntity : GeneticEntity
        {
            public double TestFitnessValue { get; set; }

            public override string Representation
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        private class TestFitnessEvalutor : FitnessEvaluator
        {
            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                return Task.FromResult(((TestEntity)entity).TestFitnessValue);
            }
        }
    }
}
