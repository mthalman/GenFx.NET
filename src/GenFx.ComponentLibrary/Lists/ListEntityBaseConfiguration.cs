using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="ListEntityBase{TConfiguration, TEntity, TItem}"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class ListEntityBaseConfiguration<TConfiguration, TEntity, TItem> : GeneticEntityConfiguration<TConfiguration, TEntity>, IListEntityBaseConfiguration
        where TConfiguration : ListEntityBaseConfiguration<TConfiguration, TEntity, TItem>
        where TEntity : ListEntityBase<TEntity, TConfiguration, TItem>
    {
    }
}
