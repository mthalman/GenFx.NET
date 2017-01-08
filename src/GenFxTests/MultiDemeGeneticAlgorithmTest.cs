using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Algorithms.MultiDemeGeneticAlgorithm and is intended
    /// to contain all GenFx.ComponentLibrary.Algorithms.MultiDemeGeneticAlgorithm Unit Tests
    /// </summary>
    [TestClass()]
    public class MultiDemeGeneticAlgorithmTest
    {
        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [TestMethod]
        public async Task MultiDemeGeneticAlgorithm_CreateNextGeneration_Async()
        {
            TestMultiDemeGeneticAlgorithm algorithm = new TestMultiDemeGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                GeneticAlgorithm = new TestMultiDemeGeneticAlgorithmFactoryConfig
                {
                    MigrantCount = 1
                },
                SelectionOperator = new MockSelectionOperatorFactoryConfig
                {
                    SelectionBasedOnFitnessType = FitnessType.Raw
                }
            });
            MockSelectionOperator selectionOp = new MockSelectionOperator(algorithm);
            algorithm.Operators.SelectionOperator = selectionOp;
            PrivateObject accessor = new PrivateObject(algorithm);

            algorithm.Environment.Populations.Add(GetPopulation(algorithm));
            algorithm.Environment.Populations.Add(GetPopulation(algorithm));
            SimplePopulation population = GetPopulation(algorithm);
            algorithm.Environment.Populations.Add(population);

            int prevPopCount = population.Entities.Count;
            await (Task)accessor.Invoke("CreateNextGenerationAsync", population);

            Assert.AreEqual(4, selectionOp.DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(prevPopCount, population.Entities.Count, "New population not created correctly.");
        }

        /// <summary>
        /// Tests that the Migrate method works correctly.
        /// </summary>
        [TestMethod]
        public void MultiDemeGeneticAlgorithm_Migrate()
        {
            MultiDemeGeneticAlgorithm algorithm = new MultiDemeGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                GeneticAlgorithm = new MultiDemeGeneticAlgorithmFactoryConfig
                {
                    MigrantCount = 2
                },
                SelectionOperator = new MockSelectionOperatorFactoryConfig
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
            });

            algorithm.Operators.SelectionOperator = new MockSelectionOperator(algorithm);
            algorithm.Operators.FitnessEvaluator = new MockFitnessEvaluator(algorithm);

            SimplePopulation population1 = new SimplePopulation(algorithm);
            population1.Entities.Add(new MockEntity(algorithm));
            population1.Entities.Add(new MockEntity(algorithm));
            population1.Entities.Add(new MockEntity(algorithm));
            population1.Entities.Add(new MockEntity(algorithm));
            population1.Entities[0].ScaledFitnessValue = 1;
            population1.Entities[1].ScaledFitnessValue = 5;
            population1.Entities[2].ScaledFitnessValue = 2;
            population1.Entities[3].ScaledFitnessValue = 4;

            SimplePopulation population2 = new SimplePopulation(algorithm);
            population2.Entities.Add(new MockEntity(algorithm));
            population2.Entities.Add(new MockEntity(algorithm));
            population2.Entities.Add(new MockEntity(algorithm));
            population2.Entities.Add(new MockEntity(algorithm));
            population2.Entities[0].ScaledFitnessValue = 6;
            population2.Entities[1].ScaledFitnessValue = 3;
            population2.Entities[2].ScaledFitnessValue = 8;
            population2.Entities[3].ScaledFitnessValue = 7;

            SimplePopulation population3 = new SimplePopulation(algorithm);
            population3.Entities.Add(new MockEntity(algorithm));
            population3.Entities.Add(new MockEntity(algorithm));
            population3.Entities.Add(new MockEntity(algorithm));
            population3.Entities.Add(new MockEntity(algorithm));
            population3.Entities[0].ScaledFitnessValue = 9;
            population3.Entities[1].ScaledFitnessValue = 13;
            population3.Entities[2].ScaledFitnessValue = 10;
            population3.Entities[3].ScaledFitnessValue = 12;

            algorithm.Environment.Populations.Add(population1);
            algorithm.Environment.Populations.Add(population2);
            algorithm.Environment.Populations.Add(population3);

            algorithm.Migrate();

            Assert.AreEqual((double)1, population1.Entities[0].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)2, population1.Entities[1].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)13, population1.Entities[2].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)12, population1.Entities[3].ScaledFitnessValue, "Incorrect entity.");

            Assert.AreEqual((double)6, population2.Entities[0].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)3, population2.Entities[1].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)5, population2.Entities[2].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)4, population2.Entities[3].ScaledFitnessValue, "Incorrect entity.");

            Assert.AreEqual((double)9, population3.Entities[0].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)10, population3.Entities[1].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)8, population3.Entities[2].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)7, population3.Entities[3].ScaledFitnessValue, "Incorrect entity.");
        }

        private static SimplePopulation GetPopulation(IGeneticAlgorithm algorithm)
        {
            SimplePopulation population = new SimplePopulation(algorithm);
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            return population;
        }

        private class TestMultiDemeGeneticAlgorithm : MultiDemeGeneticAlgorithm<TestMultiDemeGeneticAlgorithm, TestMultiDemeGeneticAlgorithmFactoryConfig>
        {
            internal bool OnMigrateCalled;

            public TestMultiDemeGeneticAlgorithm(ComponentFactoryConfigSet configurationSet)
            : base(configurationSet)
        {
            }

            protected override void OnMigrate()
            {
                this.OnMigrateCalled = true;
            }
        }

        private class TestMultiDemeGeneticAlgorithmFactoryConfig : MultiDemeGeneticAlgorithmFactoryConfig<TestMultiDemeGeneticAlgorithmFactoryConfig, TestMultiDemeGeneticAlgorithm>
        {
        }
    }
}
