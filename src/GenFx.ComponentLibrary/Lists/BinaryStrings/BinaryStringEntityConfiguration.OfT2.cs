namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Represents the configuration of <see cref="BinaryStringEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    public abstract class BinaryStringEntityConfiguration<TConfiguration, TEntity> : ListEntityBaseConfiguration<TConfiguration, TEntity, bool>
        where TConfiguration : BinaryStringEntityConfiguration<TConfiguration, TEntity> 
        where TEntity : BinaryStringEntity<TEntity, TConfiguration>
    {
    }
}
