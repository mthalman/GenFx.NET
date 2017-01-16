using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.Contracts;
using GenFx.Validation;
using System;
using System.Threading.Tasks;

namespace BinaryPatternMatching
{
    [RequiredEntity(typeof(BinaryStringEntity))]
    internal class FitnessEvaluator : FitnessEvaluatorBase
    {
        private string targetBinary;

        [ConfigurationProperty]
        [CustomValidator(typeof(BinaryStringValidator))]
        public string TargetBinary
        {
            get { return this.targetBinary; }
            set { this.SetProperty(ref this.targetBinary, value); }
        }

        public override Task<double> EvaluateFitnessAsync(IGeneticEntity entity)
        {
            BinaryStringEntity binaryEntity = (BinaryStringEntity)entity;

            int totalBitDiffs = 0;
            int minLength = Math.Min(binaryEntity.Length, this.TargetBinary.Length);
            for (int i = 0; i < minLength; i++)
            {
                bool bitValue = this.TargetBinary[i] == '0' ? false : true;
                if (binaryEntity[i] != bitValue)
                {
                    totalBitDiffs++;
                }
            }

            // add the difference in size as part of the difference in bits
            totalBitDiffs += Math.Abs(binaryEntity.Length - this.TargetBinary.Length);

            return Task.FromResult<double>(totalBitDiffs);
        }
    }
}
