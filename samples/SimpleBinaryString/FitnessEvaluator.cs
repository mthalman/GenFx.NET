using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBinaryString
{
    [RequiredEntity(typeof(FixedLengthBinaryStringEntity))]
    internal class FitnessEvaluator
        : FitnessEvaluatorBase<FitnessEvaluator, FitnessEvaluatorConfiguration>
    {
        public FitnessEvaluator(IGeneticAlgorithm algorithm) : base(algorithm)
        {
        }

        public override Task<double> EvaluateFitnessAsync(IGeneticEntity entity)
        {
            FixedLengthBinaryStringEntity binStrEntity = (FixedLengthBinaryStringEntity)entity;

            // The entity's fitness is equal to the number of "true" bits (a bit representing a 1 value)
            // it contains.
            return Task.FromResult<double>(binStrEntity.Count(bit => bit == true));
        }
    }

    internal class FitnessEvaluatorConfiguration
        : FitnessEvaluatorConfigurationBase<FitnessEvaluatorConfiguration, FitnessEvaluator>
    {
    }
}
