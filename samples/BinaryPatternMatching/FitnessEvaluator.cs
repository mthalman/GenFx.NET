using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.Contracts;
using GenFx.Validation;
using System;
using System.Threading.Tasks;

namespace BinaryPatternMatching
{
    [RequiredEntity(typeof(VariableLengthBinaryStringEntity))]
    internal class FitnessEvaluator
        : FitnessEvaluatorBase<FitnessEvaluator, FitnessEvaluatorConfiguration>
    {
        public FitnessEvaluator(IGeneticAlgorithm algorithm) : base(algorithm)
        {
        }

        public override Task<double> EvaluateFitnessAsync(IGeneticEntity entity)
        {
            VariableLengthBinaryStringEntity binaryEntity = (VariableLengthBinaryStringEntity)entity;

            int totalBitDiffs = 0;
            int minLength = Math.Min(binaryEntity.Length, this.Configuration.TargetBinary.Length);
            for (int i = 0; i < minLength; i++)
            {
                bool bitValue = this.Configuration.TargetBinary[i] == '0' ? false : true;
                if (binaryEntity[i] != bitValue)
                {
                    totalBitDiffs++;
                }
            }

            // add the difference in size as part of the difference in bits
            totalBitDiffs += Math.Abs(binaryEntity.Length - this.Configuration.TargetBinary.Length);

            return Task.FromResult<double>(totalBitDiffs);
        }
    }

    internal class FitnessEvaluatorConfiguration
        : FitnessEvaluatorFactoryConfigBase<FitnessEvaluatorConfiguration, FitnessEvaluator>
    {
        private string targetBinary;

        [CustomValidator(typeof(BinaryStringValidator))]
        public string TargetBinary
        {
            get { return this.targetBinary; }
            set { this.SetProperty(ref this.targetBinary, value); }
        }
    }
}
