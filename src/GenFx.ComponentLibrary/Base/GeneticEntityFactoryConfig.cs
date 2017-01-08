using GenFx.Contracts;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="GeneticEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    public abstract class GeneticEntityFactoryConfig<TConfiguration, TEntity> : ConfigurationForComponentWithAlgorithm<TConfiguration, TEntity>, IGeneticEntityFactoryConfig
        where TConfiguration : GeneticEntityFactoryConfig<TConfiguration, TEntity>
        where TEntity : GeneticEntity<TEntity, TConfiguration>
    {
    }
}
