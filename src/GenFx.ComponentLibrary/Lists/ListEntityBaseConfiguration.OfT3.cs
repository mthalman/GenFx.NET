namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="ListEntityBase{TConfiguration, TEntity, TItem}"/>.
    /// </summary>
    public abstract class ListEntityBaseConfiguration<TConfiguration, TEntity, TItem> : ListEntityBaseConfiguration<TConfiguration, TEntity>
        where TConfiguration : ListEntityBaseConfiguration<TConfiguration, TEntity>
        where TEntity : ListEntityBase<TEntity, TConfiguration>
    {
    }
}
