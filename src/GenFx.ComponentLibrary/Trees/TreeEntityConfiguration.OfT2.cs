using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents the configuration of <see cref="TreeEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    public abstract class TreeEntityConfiguration<TConfiguration, TEntity> : GeneticEntityConfiguration<TConfiguration, TEntity>
        where TConfiguration : TreeEntityConfiguration<TConfiguration, TEntity> 
        where TEntity : TreeEntity<TEntity, TConfiguration>
    {
    }
}
