using GenFx;
using GenFx.ComponentLibrary.Lists;
using GenFx.Validation;

namespace BinaryPatternMatching
{
    [RequiredGeneticEntity(typeof(BinaryStringEntity))]
    public class BinaryStringMutationOperator : UniformBitMutationOperator
    {
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            bool isMutated = false;

            // In addition to the base mutation implementation, each bit has a
            // probability (equal to the mutation rate) of being removed.
            BinaryStringEntity binaryEntity = (BinaryStringEntity)entity;
            for (int i = binaryEntity.Length - 1; i >= 0; i--)
            {
                if (RandomNumberService.Instance.GetDouble() <= this.MutationRate)
                {
                    binaryEntity.RemoveAt(i);
                    isMutated = true;
                }
            }

            if (base.GenerateMutation(entity))
            {
                isMutated = true;
            }

            return isMutated;
        }
    }
}
