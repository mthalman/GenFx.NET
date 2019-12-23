using GenFx;
using GenFx.Components.Algorithms;
using GenFx.Components.Lists;
using GenFx.Components.Metrics;
using GenFx.Components.Populations;
using GenFx.Components.SelectionOperators;
using GenFx.Components.Terminators;
using GenFx.UI;
using GenFx.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace UISampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }
    }

    public class ViewModel : ObservableObject
    {
        private int currentGeneration;

        public ExecutionContext Context { get; private set; }

        public int CurrentGeneration
        {
            get { return this.currentGeneration; }
            set { this.SetProperty(ref this.currentGeneration, value); }
        }
        
        public ViewModel()
        {
            SimpleGeneticAlgorithm algorithm = new SimpleGeneticAlgorithm
            {
                MinimumEnvironmentSize = 3,
                FitnessEvaluator = new FitnessEvaluator(),
                GeneticEntitySeed = new BinaryStringEntity
                {
                    MinimumStartingLength = 20,
                    MaximumStartingLength = 20,
                    IsFixedSize = true
                },
                PopulationSeed = new SimplePopulation
                {
                    MinimumPopulationSize = 100
                },
                SelectionOperator = new FitnessProportionateSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Raw
                },
                CrossoverOperator = new SinglePointCrossoverOperator
                {
                    CrossoverRate = 0.8
                },
                MutationOperator = new UniformBitMutationOperator
                {
                    MutationRate = 0.01
                },
                //Terminator = new GenerationalTerminator
                //{
                //    FinalGeneration = 5
                //}
            };

            algorithm.Metrics.Add(new MeanFitness());
            algorithm.Metrics.Add(new MaximumFitness());

            algorithm.GenerationCreated += Algorithm_GenerationCreated;

            this.Context = new ExecutionContext(algorithm);
        }

        private void Algorithm_GenerationCreated(object sender, System.EventArgs e)
        {
            this.CurrentGeneration = this.Context.GeneticAlgorithm.CurrentGeneration;
        }
    }

    [RequiredGeneticEntity(typeof(BinaryStringEntity))]
    internal class FitnessEvaluator : GenFx.FitnessEvaluator
    {
        public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
        {
            BinaryStringEntity binStrEntity = (BinaryStringEntity)entity;

            // The entity's fitness is equal to the number of "true" bits (a bit representing a 1 value)
            // it contains.
            return Task.FromResult<double>(binStrEntity.Count(bit => bit == true));
        }
    }
}
