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
    }
}
