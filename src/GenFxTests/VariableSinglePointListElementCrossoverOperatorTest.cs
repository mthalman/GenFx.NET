using GenFx;
using GenFx.ComponentLibrary.Lists;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.Contracts;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for <see cref="VariableSinglePointListElementCrossoverOperator"/>. 
    /// </summary>
    [TestClass()]
    public class VariableVariableSinglePointBitCrossoverOperatorTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Crossover method works correctly.
        /// </summary>
        [TestMethod]
        public void VariableSinglePointBitCrossoverOperator_Crossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                CrossoverOperator = new VariableSinglePointListElementCrossoverOperatorFactoryConfig
                {
                    CrossoverRate = 1
                },
                Entity = new VariableLengthBinaryStringEntityFactoryConfig
                {
                    MinimumStartingLength = 4,
                    MaximumStartingLength = 6
                }
            });

            VariableSinglePointListElementCrossoverOperator op = new VariableSinglePointListElementCrossoverOperator(algorithm);
            VariableLengthBinaryStringEntity entity1 = new VariableLengthBinaryStringEntity(algorithm);
            entity1.Initialize();
            entity1.Length = 4;
            entity1[0] = true;
            entity1[1] = false;
            entity1[2] = false;
            entity1[3] = true;

            VariableLengthBinaryStringEntity entity2 = new VariableLengthBinaryStringEntity(algorithm);
            entity2.Initialize();
            entity2.Length = 6;
            entity2[0] = true;
            entity2[1] = true;
            entity2[2] = false;
            entity2[3] = false;
            entity2[4] = false;
            entity2[5] = true;

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomVal1 = 1;
            randomUtil.RandomVal2 = 5;
            IList<IGeneticEntity> result = op.Crossover(entity1, entity2);

            VariableLengthBinaryStringEntity resultEntity1 = (VariableLengthBinaryStringEntity)result[0];
            VariableLengthBinaryStringEntity resultEntity2 = (VariableLengthBinaryStringEntity)result[1];

            Assert.AreEqual("11", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("11000001", resultEntity2.Representation, "Crossover not correct.");

            randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;
            randomUtil.RandomVal1 = 3;
            randomUtil.RandomVal2 = 2;
            result = op.Crossover(entity1, entity2);

            resultEntity1 = (VariableLengthBinaryStringEntity)result[0];
            resultEntity2 = (VariableLengthBinaryStringEntity)result[1];

            Assert.AreEqual("1000001", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("111", resultEntity2.Representation, "Crossover not correct.");
        }

        private class TestRandomUtil : IRandomNumberService
        {
            internal int RandomVal1;
            internal int RandomVal2;

            private bool calledOnce = false;

            public int GetRandomValue(int maxValue)
            {
                if (!calledOnce)
                {
                    calledOnce = true;
                    return RandomVal1;
                }
                return RandomVal2;
            }

            public double GetRandomPercentRatio()
            {
                return new RandomNumberService().GetRandomPercentRatio();
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                return 1;
            }
        }

    }


}
