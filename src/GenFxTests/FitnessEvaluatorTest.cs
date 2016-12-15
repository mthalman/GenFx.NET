using GenFx;
using GenFx.ComponentModel;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.FitnessEvaluator and is intended
    /// to contain all GenFx.FitnessEvaluator Unit Tests
    /// </summary>
    [TestClass()]
    public class FitnessEvaluatorTest
    {

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void FitnessEvaluator_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new MockFitnessEvaluator(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting class is missing.
        /// </summary>
        [TestMethod]
        public void FitnessEvaluator_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new FakeFitnessEvaluator(new MockGeneticAlgorithm()));
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task FitnessEvaluator_EvaluateFitness_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = new FakeFitnessEvaluator2Configuration();
            FakeFitnessEvaluator2 evaluator = new FakeFitnessEvaluator2(algorithm);
            MockEntity entity = new MockEntity(algorithm);
            double actualVal = await evaluator.EvaluateFitnessAsync(entity);

            Assert.AreEqual((double)99, actualVal, "Fitness was not evaluated correctly.");
        }

        private class FakeFitnessEvaluator : FitnessEvaluator
        {
            public FakeFitnessEvaluator(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [Component(typeof(FakeFitnessEvaluator))]
        private class FakeFitnessEvaluatorConfiguration : FitnessEvaluatorConfiguration
        {
        }

        private class FakeFitnessEvaluator2 : FitnessEvaluator
        {
            public FakeFitnessEvaluator2(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                return Task.FromResult((double)99);
            }
        }

        [Component(typeof(FakeFitnessEvaluator2))]
        private class FakeFitnessEvaluator2Configuration : FitnessEvaluatorConfiguration
        {
        }
    }


}
