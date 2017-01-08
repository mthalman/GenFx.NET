using GenFx.Contracts;
using GenFx.Validation;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the configuration of <see cref="MultiDemeGeneticAlgorithm{TAlgorithm, TConfiguration}"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    public abstract class MultiDemeGeneticAlgorithmFactoryConfig<TConfiguration, TAlgorithm> : GeneticAlgorithmFactoryConfig<TConfiguration, TAlgorithm>
        where TConfiguration : MultiDemeGeneticAlgorithmFactoryConfig<TConfiguration, TAlgorithm>
        where TAlgorithm : MultiDemeGeneticAlgorithm<TAlgorithm, TConfiguration>
    {
        private const int DefaultMigrantCount = 0;
        private const int DefaultMigrateEachGeneration = 1;

        private int migrantCount = DefaultMigrantCount;
        private int migrateEachGeneration = DefaultMigrateEachGeneration;

        /// <summary>
        /// Gets or sets the value indicating how many <see cref="IGeneticEntity"/> objects are migrated between
        /// populations each generation.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [DefaultValue(DefaultMigrantCount)]
        [IntegerValidator(MinValue = 0)]
        public int MigrantCount
        {
            get { return this.migrantCount; }
            set { this.SetProperty(ref this.migrantCount, value); }
        }

        /// <summary>
        /// Gets or sets how many generations go by before each migration occurs.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [DefaultValue(DefaultMigrateEachGeneration)]
        [IntegerValidator(MinValue = 1)]
        public int MigrateEachGeneration
        {
            get { return this.migrateEachGeneration; }
            set { this.SetProperty(ref this.migrateEachGeneration, value); }
        }
    }
}
