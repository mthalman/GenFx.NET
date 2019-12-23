using GenFx;
using GenFx.Components.Lists;
using GenFx.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SimpleBinaryString
{
    [DataContract]
    [RequiredGeneticEntity(typeof(BinaryStringEntity))]
    [IntegerExternalValidator(typeof(GeneticAlgorithm), nameof(GeneticAlgorithm.MinimumEnvironmentSize), MaxValue = 1)]
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
