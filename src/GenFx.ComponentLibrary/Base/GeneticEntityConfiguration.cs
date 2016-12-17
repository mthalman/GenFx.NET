using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="GeneticEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    public abstract class GeneticEntityConfiguration<TConfiguration, TEntity> : ComponentConfiguration<TConfiguration, TEntity>, IGeneticEntityConfiguration
        where TConfiguration : GeneticEntityConfiguration<TConfiguration, TEntity>
        where TEntity : GeneticEntity<TEntity, TConfiguration>
    {
    }
}
