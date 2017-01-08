using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents the configuration of <see cref="TreeEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    public abstract class TreeEntityFactoryConfig<TConfiguration, TEntity> : GeneticEntityFactoryConfig<TConfiguration, TEntity>
        where TConfiguration : TreeEntityFactoryConfig<TConfiguration, TEntity> 
        where TEntity : TreeEntity<TEntity, TConfiguration>
    {
    }
}
