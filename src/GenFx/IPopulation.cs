using GenFx.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Represents a collection of <see cref="IGeneticEntity"/> objects which interact locally with each other.  A population is 
    /// the unit from which the <see cref="ISelectionOperator"/> selects its genetic entities.
    /// </summary>
    public interface IPopulation : IGeneticComponent
    {
        /// <summary>
        /// Gets the collection of <see cref="IGeneticEntity"/> objects contained by the population.
        /// </summary>
        ObservableCollection<IGeneticEntity> Entities { get; }

        /// <summary>
        /// Gets or sets the index of this population in the <see cref="GeneticEnvironment"/>.
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Evaluates the <see cref="IGeneticEntity.RawFitnessValue"/> of all the <see cref="IGeneticEntity"/> objects
        /// within the population followed by evaluation of the <see cref="IGeneticEntity.ScaledFitnessValue"/>
        /// using the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        Task EvaluateFitnessAsync();

        /// <summary>
        /// Creates the collection of <see cref="IGeneticEntity"/> objects contained by this population.
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Gets the mean of all the <see cref="IGeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The mean of all the <see cref="IGeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        double RawMean { get; }

        /// <summary>
        /// Gets the standard deviation of all the <see cref="IGeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The standard deviation of all the <see cref="IGeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        double RawStandardDeviation { get; }

        /// <summary>
        /// Gets the maximum <see cref="IGeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The maximum <see cref="IGeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        double RawMax { get; }

        /// <summary>
        /// Gets the minimum <see cref="IGeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The minimum <see cref="IGeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        double RawMin { get; }

        /// <summary>
        /// Gets the mean of all the <see cref="IGeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The mean of all the <see cref="IGeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        double ScaledMean { get; }

        /// <summary>
        /// Gets the standard deviation of all the <see cref="IGeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The standard deviation of all the <see cref="IGeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        double ScaledStandardDeviation { get; }

        /// <summary>
        /// Gets the maximum <see cref="IGeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The maximum <see cref="IGeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        double ScaledMax { get; }

        /// <summary>
        /// Gets the minimum <see cref="IGeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The minimum <see cref="IGeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        double ScaledMin { get; }
    }
}
