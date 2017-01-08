﻿using GenFx;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.Contracts;
using GenFx.Validation;

namespace BinaryPatternMatching
{
    [RequiredEntity(typeof(VariableLengthBinaryStringEntity))]
    public class BinaryStringMutationOperator : UniformBitMutationOperator<BinaryStringMutationOperator, BinaryStringMutationOperatorConfiguration>
    {
        public BinaryStringMutationOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override bool GenerateMutation(IGeneticEntity entity)
        {
            bool isMutated = false;

            // In addition to the base mutation implementation, each bit has a
            // probability (equal to the mutation rate) of being removed.
            VariableLengthBinaryStringEntity binaryEntity = (VariableLengthBinaryStringEntity)entity;
            for (int i = binaryEntity.Length - 1; i >= 0; i--)
            {
                if (RandomNumberService.Instance.GetRandomPercentRatio() <= this.Configuration.MutationRate)
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

    public class BinaryStringMutationOperatorConfiguration
        : UniformBitMutationOperatorFactoryConfig<BinaryStringMutationOperatorConfiguration, BinaryStringMutationOperator>
    {
    }
}
