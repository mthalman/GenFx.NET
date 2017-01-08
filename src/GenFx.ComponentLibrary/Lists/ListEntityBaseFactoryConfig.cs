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
    public abstract class ListEntityBaseFactoryConfig<TConfiguration, TEntity, TItem> : GeneticEntityFactoryConfig<TConfiguration, TEntity>, IListEntityBaseFactoryConfig
        where TConfiguration : ListEntityBaseFactoryConfig<TConfiguration, TEntity, TItem>
        where TEntity : ListEntityBase<TEntity, TConfiguration, TItem>
    {
    }
}
