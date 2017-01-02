using GenFx.ComponentLibrary.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="GeneticEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    public abstract class GeneticEntityConfiguration<TConfiguration, TEntity> : ConfigurationForComponentWithAlgorithm<TConfiguration, TEntity>, IGeneticEntityConfiguration
        where TConfiguration : GeneticEntityConfiguration<TConfiguration, TEntity>
        where TEntity : GeneticEntity<TEntity, TConfiguration>
    {
    }
}
