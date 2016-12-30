namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="ListEntity{TConfiguration, TEntity, TItem}"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class ListEntityConfiguration<TConfiguration, TEntity, TItem> : ListEntityBaseConfiguration<TConfiguration, TEntity, TItem>, IListEntityConfiguration<TItem>
        where TConfiguration : ListEntityConfiguration<TConfiguration, TEntity, TItem>
        where TEntity : ListEntity<TEntity, TConfiguration, TItem>
    {
    }
}
