using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.Contracts;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.BinaryStrings.BitInversionOperator and is intended
    ///to contain all GenFx.ComponentLibrary.BinaryStrings.BitInversionOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class InversionOperatorTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [TestMethod]
        public void BitInversionOperator_Mutate()
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new FixedLengthBinaryStringEntity
                {
                    FixedLength = 4
                },
                MutationOperator = new InversionOperator
                {
                    MutationRate = 1
                }
            };
            algorithm.GeneticEntitySeed.Initialize(algorithm);

            InversionOperator op = (InversionOperator)algorithm.MutationOperator;
            op.Initialize(algorithm);
            FixedLengthBinaryStringEntity entity = (FixedLengthBinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity.Age = 10;
            entity.Initialize(algorithm);
            entity[0] = true;
            entity[1] = true;
            entity[2] = false;
            entity[3] = true;

            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;
            randomUtil.RandomValue = 1;

            IGeneticEntity mutant = op.Mutate(entity);

            Assert.AreEqual("1011", mutant.Representation, "Mutation not called correctly.");
            Assert.AreEqual(0, mutant.Age, "Age should have been reset.");
        }

        private class FakeRandomUtil : IRandomNumberService
        {
            public int RandomValue;

            public int GetRandomValue(int maxValue)
            {
                return this.RandomValue++;
            }

            public double GetDouble()
            {
                return 1;
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
