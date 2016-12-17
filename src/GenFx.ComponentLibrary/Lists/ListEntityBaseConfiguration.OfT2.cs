using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="ListEntityBase{TEntity, TConfiguration}"/>.
    /// </summary>
    public abstract class ListEntityBaseConfiguration<TConfiguration, TEntity> : GeneticEntityConfiguration<TConfiguration, TEntity>
        where TConfiguration : ListEntityBaseConfiguration<TConfiguration, TEntity>
        where TEntity : ListEntityBase<TEntity, TConfiguration>
        
    {
    }
}
