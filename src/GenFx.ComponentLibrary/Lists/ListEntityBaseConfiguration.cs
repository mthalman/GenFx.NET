using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="ListEntityBase{TConfiguration, TEntity, TItem}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    /// <typeparam name="TItem">Type of the values contained in the list.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class ListEntityBaseConfiguration<TConfiguration, TEntity, TItem> : GeneticEntityConfiguration<TConfiguration, TEntity>, IListEntityBaseConfiguration
        where TConfiguration : ListEntityBaseConfiguration<TConfiguration, TEntity, TItem>
        where TEntity : ListEntityBase<TEntity, TConfiguration, TItem>
    {
    }
}
