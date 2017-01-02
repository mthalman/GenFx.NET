using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents the configuration of <see cref="TreeEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    public abstract class TreeEntityConfiguration<TConfiguration, TEntity> : GeneticEntityConfiguration<TConfiguration, TEntity>
        where TConfiguration : TreeEntityConfiguration<TConfiguration, TEntity> 
        where TEntity : TreeEntity<TEntity, TConfiguration>
    {
    }
}
