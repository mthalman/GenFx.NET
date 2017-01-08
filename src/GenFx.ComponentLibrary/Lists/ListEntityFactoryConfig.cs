namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="ListEntity{TConfiguration, TEntity, TItem}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    /// <typeparam name="TItem">Type of the values contained in the list.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class ListEntityFactoryConfig<TConfiguration, TEntity, TItem> : ListEntityBaseFactoryConfig<TConfiguration, TEntity, TItem>, IListEntityConfiguration<TItem>
        where TConfiguration : ListEntityFactoryConfig<TConfiguration, TEntity, TItem>
        where TEntity : ListEntity<TEntity, TConfiguration, TItem>
    {
    }
}
