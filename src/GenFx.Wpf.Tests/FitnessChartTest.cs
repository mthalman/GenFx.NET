using GenFx.Wpf.Controls;
using Moq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCommon;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="FitnessChart"/> class.
    /// </summary>
    public class FitnessChartTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [StaFact]
        public void FitnessChart_Ctor()
        {
            FitnessChart chart = new FitnessChart();
            Assert.Null(chart.Population);
            Assert.Equal(FitnessType.Scaled, chart.FitnessType);
            Assert.Equal(FitnessSortOption.Entity, chart.FitnessSortOption);

            PlotModel model = chart.PlotModel;

            Assert.Equal(2, model.Axes.Count);
            Assert.IsType<CategoryAxis>(model.Axes[0]);
            Assert.IsType<LinearAxis>(model.Axes[1]);

            Assert.Single(model.Series);
            Assert.IsType<ColumnSeries>(model.Series[0]);
        }

        /// <summary>
        /// Tests that the chart is refreshed when <see cref="FitnessChart.FitnessType"/> changes.
        /// </summary>
        [StaFact]
        public void FitnessChart_OnFitnessTypeChanged()
        {
            FitnessChart chart = new FitnessChart();
            PlotModel model = chart.PlotModel;

            CategoryAxis axis = (CategoryAxis)model.Axes[0];
            axis.Labels.Add("test");

            chart.FitnessType = FitnessType.Raw;

            Assert.Empty(axis.Labels);
        }

        /// <summary>
        /// Tests that the chart is refreshed when <see cref="FitnessChart.FitnessSortOption"/> changes.
        /// </summary>
        [StaFact]
        public void FitnessChart_OnFitnessSortOptionChanged()
        {
            FitnessChart chart = new FitnessChart();
            PlotModel model = chart.PlotModel;

            CategoryAxis axis = (CategoryAxis)model.Axes[0];
            axis.Labels.Add("test");

            chart.FitnessSortOption = FitnessSortOption.Fitness;

            Assert.Empty(axis.Labels);
        }

        /// <summary>
        /// Tests that the chart is not refreshed when <see cref="FitnessChart.Population"/> is set to a
        /// non-populated population.
        /// </summary>
        [StaFact]
        public void FitnessChart_OnPopulationChanged_NonPopulated()
        {
            FitnessChart chart = new FitnessChart();

            PlotModel model = chart.PlotModel;
            CategoryAxis axis = (CategoryAxis)model.Axes[0];
            axis.Labels.Add("test");

            TestPopulation population = new TestPopulation();
            population.Initialize(Mock.Of<GeneticAlgorithm>());
            chart.Population = population;

            Assert.Equal("test", axis.Labels[0]);
        }

        /// <summary>
        /// Tests that the chart is refreshed when <see cref="FitnessChart.Population"/> is set to a
        /// populated population.
        /// </summary>
        [StaFact]
        public async Task FitnessChart_OnPopulationChanged_Populated()
        {
            await TestRefreshChartScenario(FitnessType.Scaled, FitnessSortOption.Entity, true, true, true, true);
            await TestRefreshChartScenario(FitnessType.Raw, FitnessSortOption.Entity, false, false, true, true);
            await TestRefreshChartScenario(FitnessType.Scaled, FitnessSortOption.Fitness, true, false, false, true);
            await TestRefreshChartScenario(FitnessType.Raw, FitnessSortOption.Fitness, false, true, false, false);
        }

        private async Task TestRefreshChartScenario(FitnessType fitnessType, FitnessSortOption fitnessSortOption,
            bool switchPopulations, bool createNewGeneration, bool completeAlgorithm, bool elapseTime)
        {
            TimeSpan elapsedTime = TimeSpan.FromSeconds(0);
            Mock<IStopwatch> stopwatchMock = new Mock<IStopwatch>();
            stopwatchMock
                .SetupGet(o => o.Elapsed)
                .Returns(() => elapsedTime);
            Mock<IStopwatchFactory> stopwatchFactoryMock = new Mock<IStopwatchFactory>();
            stopwatchFactoryMock
                .Setup(o => o.Create())
                .Returns(stopwatchMock.Object);

            FitnessChart chart = new FitnessChart(stopwatchFactoryMock.Object)
            {
                FitnessSortOption = fitnessSortOption,
                FitnessType = fitnessType
            };

            PlotModel model = chart.PlotModel;
            CategoryAxis axis = (CategoryAxis)model.Axes[0];
            axis.Labels.Add("test"); // Add test label to ensure it gets cleared

            TestAlgorithm algorithm = new TestAlgorithm
            {
                MinimumEnvironmentSize = 2,
                PopulationSeed = new TestPopulation()
                {
                    MinimumPopulationSize = 5
                },
                FitnessEvaluator = new TestFitnessEvaluator(),
                GeneticEntitySeed = new TestEntity(),
                SelectionOperator = new TestSelectionOperator(),
                Terminator = new TestTerminator()
            };

            await algorithm.InitializeAsync();

            if (switchPopulations)
            {
                chart.Population = algorithm.Environment.Populations[1];
            }

            Population population = algorithm.Environment.Populations[0];
            TestEntity[] entities = population.Entities.Cast<TestEntity>().ToArray();
            entities[0].RawFitnessValue = 2;
            entities[1].RawFitnessValue = 0;
            entities[2].RawFitnessValue = 1;
            entities[3].RawFitnessValue = 4;
            entities[4].RawFitnessValue = 3;

            entities[0].ScaledFitnessValue = 0;
            entities[1].ScaledFitnessValue = 3;
            entities[2].ScaledFitnessValue = 2;
            entities[3].ScaledFitnessValue = 4;
            entities[4].ScaledFitnessValue = 1;

            entities[0].CompareFactor = 4;
            entities[1].CompareFactor = 0;
            entities[2].CompareFactor = 3;
            entities[3].CompareFactor = 2;
            entities[4].CompareFactor = 1;

            // Set the Population which will trigger the logic to test
            chart.Population = population;

            stopwatchFactoryMock.Verify(o => o.Create(), Times.Once());
            stopwatchMock.Verify(o => o.Start(), Times.Once());
            stopwatchMock.Verify(o => o.Restart(), switchPopulations ? Times.Exactly(2) : Times.Once());

            List<GeneticEntity> sortedEntities;
            if (fitnessSortOption == FitnessSortOption.Entity)
            {
                sortedEntities = new List<GeneticEntity>
                {
                    entities[1],
                    entities[4],
                    entities[3],
                    entities[2],
                    entities[0],
                };
            }
            else
            {
                if (fitnessType == FitnessType.Raw)
                {
                    sortedEntities = new List<GeneticEntity>
                    {
                        entities[1],
                        entities[2],
                        entities[0],
                        entities[4],
                        entities[3],
                    };
                }
                else
                {
                    sortedEntities = new List<GeneticEntity>
                    {
                        entities[0],
                        entities[4],
                        entities[2],
                        entities[1],
                        entities[3],
                    };
                }
            }
            
            ColumnSeries columnSeries = (ColumnSeries)model.Series[0];

            if (fitnessType == FitnessType.Scaled)
            {
                Assert.Equal(nameof(GeneticEntity.ScaledFitnessValue), columnSeries.ValueField);
            }
            else
            {
                Assert.Equal(nameof(GeneticEntity.RawFitnessValue), columnSeries.ValueField);
            }
            
            Assert.Equal(sortedEntities, columnSeries.ItemsSource.Cast<object>().ToList());

            Assert.Equal(algorithm.PopulationSeed.MinimumPopulationSize, axis.Labels.Count);
            for (int i = 0; i < algorithm.PopulationSeed.MinimumPopulationSize; i++)
            {
                Assert.Equal("", axis.Labels[i]);
            }

            if (createNewGeneration)
            {
                if (elapseTime)
                {
                    // Ensure that enough time has passed for refresh to occur
                    elapsedTime = TimeSpan.FromSeconds(5);
                }
                
                // Create next generation to cause the chart to be refresh again
                await algorithm.StepAsync();

                if (elapseTime)
                {
                    Assert.NotEqual(sortedEntities,
                        columnSeries.ItemsSource.Cast<object>().ToList());
                }
            }
            
            if (completeAlgorithm)
            {
                if (elapseTime)
                {
                    // Ensure that enough time has passed for refresh to occur
                    elapsedTime = TimeSpan.FromSeconds(5);
                }

                ((TestTerminator)algorithm.Terminator).IsCompleteValue = true;
                // Create next generation to cause the chart to be refresh again
                await algorithm.StepAsync();

                if (createNewGeneration && elapseTime)
                {
                    Assert.NotEqual(sortedEntities,
                        columnSeries.ItemsSource.Cast<object>().ToList());
                }
                else
                {
                    Assert.Equal(sortedEntities,
                        columnSeries.ItemsSource.Cast<object>().ToList());
                }
            }

            // Set the population to null to verify the series gets cleared
            chart.Population = null;
            Assert.Null(columnSeries.ItemsSource);
        }

        private class TestSelectionOperator : SelectionOperator
        {
            protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
            {
                return population.Entities.Take(entityCount);
            }
        }

        private class TestTerminator : Terminator
        {
            public bool IsCompleteValue;

            public override bool IsComplete()
            {
                return this.IsCompleteValue;
            }
        }

        private class TestEntity : GeneticEntity
        {
            public new double RawFitnessValue
            {
                get { return base.RawFitnessValue; }
                set
                {
                    PrivateObject obj = new PrivateObject(this, new PrivateType(typeof(GeneticEntity)));
                    obj.SetField("rawFitnessValue", value);
                }
            }

            public int CompareFactor { get; set; }

            public override string Representation
            {
                get
                {
                    return this.RawFitnessValue.ToString();
                }
            }

            public override int CompareTo(GeneticEntity other)
            {
                return this.CompareFactor.CompareTo(((TestEntity)other).CompareFactor);
            }
        }

        private class TestFitnessEvaluator : FitnessEvaluator
        {
            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                return Task.FromResult<double>(0);
            }
        }

        private class TestAlgorithm : GeneticAlgorithm
        {
            protected override Task CreateNextGenerationAsync(Population population)
            {
                population.Entities.Clear();
                for (int i = 0; i < population.MinimumPopulationSize; i++)
                {
                    population.Entities.Add((GeneticEntity)this.GeneticEntitySeed.CreateNewAndInitialize());
                }

                return Task.FromResult(0);
            }
        }

        private class TestPopulation : Population
        {
        }
    }
}
