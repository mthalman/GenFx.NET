using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.Contracts;
using GenFx.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBinaryString
{
    [RequiredEntity(typeof(BinaryStringEntity))]
    internal class FitnessEvaluator : FitnessEvaluatorBase
    {
        public override Task<double> EvaluateFitnessAsync(IGeneticEntity entity)
        {
            BinaryStringEntity binStrEntity = (BinaryStringEntity)entity;

            // The entity's fitness is equal to the number of "true" bits (a bit representing a 1 value)
            // it contains.
            return Task.FromResult<double>(binStrEntity.Count(bit => bit == true));
        }
    }
}
