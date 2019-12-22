using GenFx.ComponentLibrary.Terminators;
using GenFx.Validation;
using System;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Terminators.GenerationalTerminator and is intended
    /// to contain all GenFx.ComponentLibrary.Terminators.GenerationalTerminator Unit Tests
    /// </summary>
    public class GenerationalTerminatorTest
    {
        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for a required setting.
        /// </summary>
        [Fact]
        public void GenerationalTerminator_Ctor_InvalidSetting1()
        {
            GenerationalTerminator config = new GenerationalTerminator();
            Assert.Throws<ValidationException>(() => config.FinalGeneration = -1);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for a required setting.
        /// </summary>
        [Fact]
        public void GenerationalTerminator_Ctor_InvalidSetting2()
        {
            GenerationalTerminator config = new GenerationalTerminator();
            Assert.Throws<ValidationException>(() => config.FinalGeneration = 0);
        }

        /// <summary>
        /// Tests that the IsComplete method works correctly.
        /// </summary>
        [Fact]
        public void GenerationalTerminator_IsComplete()
        {
            int finalGeneration = 10;
            GeneticAlgorithm algorithm = GetAlgorithm(finalGeneration);
            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            GenerationalTerminator terminator = (GenerationalTerminator)algorithm.Terminator;
            terminator.Initialize(algorithm);
            Assert.False(terminator.IsComplete(), "Should not be complete at generation 0.");
            accessor.SetField("currentGeneration", (int)accessor.GetField("currentGeneration") + 1);
            Assert.False(terminator.IsComplete(), "Should not be complete at generation 1.");
            accessor.SetField("currentGeneration", finalGeneration);
            Assert.True(terminator.IsComplete(), "Should be complete.");
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void GenerationalTerminator_Serialization()
        {
            GenerationalTerminator terminator = new GenerationalTerminator();
            terminator.FinalGeneration = 5;

            GenerationalTerminator result = (GenerationalTerminator)SerializationHelper.TestSerialization(terminator, new Type[0]);

            Assert.Equal(terminator.FinalGeneration, result.FinalGeneration);
        }

        private static GeneticAlgorithm GetAlgorithm(int finalGeneration)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                Terminator = new GenerationalTerminator
                {
                    FinalGeneration = finalGeneration
                }
            };
            return algorithm;
        }
    }
}
