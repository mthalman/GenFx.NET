using GenFx.UI.Controls;
using GenFx.UI.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="PopulationSelector"/> class.
    /// </summary>
    [TestClass]
    public class PopulationSelectorTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_Ctor()
        {
            PopulationSelector selector = new PopulationSelector();
            Assert.IsNull(selector.SelectedPopulation);
            Assert.AreEqual(-1, selector.SelectedPopulationIndex);
            Assert.IsNull(selector.Environment);
        }

        /// <summary>
        /// Tests that no change is made when the enviroment is changed without a population.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_OnEnvironmentChanged_NoPopulation()
        {
            PopulationSelector selector = new PopulationSelector();
            selector.Environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Assert.IsNull(selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is changed when the environment is changed with a population.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_OnEnvironmentChanged_WithPopulation()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);

            selector.Environment = environment;

            Assert.AreSame(population, selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is changed when the environment is replaced.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_OnEnvironmentChanged_ReplaceEnvironment()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);

            selector.Environment = environment;

            GeneticEnvironment environment2 = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population2 = Mock.Of<Population>();
            environment2.Populations.Add(population2);

            selector.Environment = environment2;

            Assert.AreSame(population2, selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is changed when the environment is removed.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_OnEnvironmentChanged_RemoveEnvironment()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);

            selector.Environment = environment;

            selector.Environment = null;

            Assert.IsNull(selector.SelectedPopulation);
            Assert.AreEqual(-1, selector.SelectedPopulationIndex);

            // Verify the control doesn't respond to a population being added to removed environment
            environment.Populations.Add(Mock.Of<Population>());

            Assert.IsNull(selector.SelectedPopulation);
            Assert.AreEqual(-1, selector.SelectedPopulationIndex);
        }

        /// <summary>
        /// Tests that the selected population is changed to the non-default index when the environment is changed with a population.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_OnEnvironmentChanged_WithPopulation_ChangedIndex()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);

            Population population2 = Mock.Of<Population>();
            environment.Populations.Add(population2);

            selector.SelectedPopulationIndex = 1;
            selector.Environment = environment;

            Assert.AreSame(population2, selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is changed when an intial population is later added to the environment.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_InitialPopulationAdded()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            selector.Environment = environment;

            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);
            DispatcherHelper.DoEvents();

            Assert.AreSame(population, selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is not changed when a non-initial population is later added to the environment.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_NonInitialPopulationAdded()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);

            selector.Environment = environment;

            Population population2 = Mock.Of<Population>();
            environment.Populations.Add(population2);
            DispatcherHelper.DoEvents();

            Assert.AreSame(population, selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is changed when the currently selected population has been removed.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_SelectedPopulationRemoved()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);

            selector.Environment = environment;

            environment.Populations.RemoveAt(0);
            DispatcherHelper.DoEvents();

            Assert.IsNull(selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is changed when the currently selected population has been removed.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_SelectedPopulationUpdatedOnRemoval()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);
            Population population2 = Mock.Of<Population>();
            environment.Populations.Add(population2);

            selector.Environment = environment;

            environment.Populations.RemoveAt(0);
            DispatcherHelper.DoEvents();

            Assert.AreSame(population2, selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is changed when the currently selected population has been removed.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_SelectedPopulationUpdatedOnRemoval_NonDefaultIndex()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);
            Population population2 = Mock.Of<Population>();
            environment.Populations.Add(population2);

            selector.Environment = environment;
            selector.SelectedPopulationIndex = 1;

            environment.Populations.RemoveAt(1);
            DispatcherHelper.DoEvents();

            Assert.AreSame(population, selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is changed when the index is changed.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_SelectedPopulationIndexChanged()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);
            Population population2 = Mock.Of<Population>();
            environment.Populations.Add(population2);

            selector.Environment = environment;

            Assert.AreSame(population, selector.SelectedPopulation);

            selector.SelectedPopulationIndex = 1;

            Assert.AreSame(population2, selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population set to null when index is changed but no environment is set.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_SelectedPopulationIndexChanged_NoEnvironment()
        {
            PopulationSelector selector = new PopulationSelector();
            selector.SelectedPopulationIndex = 1;
            Assert.IsNull(selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is set to null when the index is changed to less than zero.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_SelectedPopulationIndexChanged_LessThanZero()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);

            selector.Environment = environment;

            Assert.AreSame(population, selector.SelectedPopulation);

            selector.SelectedPopulationIndex = -1;

            Assert.IsNull(selector.SelectedPopulation);
        }

        /// <summary>
        /// Tests that the selected population is set to null when the index is set greater than available index.
        /// </summary>
        [TestMethod]
        public void PopulationSelector_SelectedPopulationIndexChanged_GreaterThanAllowed()
        {
            PopulationSelector selector = new PopulationSelector();

            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            Population population = Mock.Of<Population>();
            environment.Populations.Add(population);

            selector.Environment = environment;

            Assert.AreSame(population, selector.SelectedPopulation);

            selector.SelectedPopulationIndex = 1;

            Assert.IsNull(selector.SelectedPopulation);
        }
    }
}
