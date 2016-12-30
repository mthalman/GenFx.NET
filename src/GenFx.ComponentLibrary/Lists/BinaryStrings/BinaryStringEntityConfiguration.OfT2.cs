namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Represents the configuration of <see cref="BinaryStringEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    public abstract class BinaryStringEntityConfiguration<TConfiguration, TEntity> : ListEntityBaseConfiguration<TConfiguration, TEntity, bool>
        where TConfiguration : BinaryStringEntityConfiguration<TConfiguration, TEntity> 
        where TEntity : BinaryStringEntity<TEntity, TConfiguration>
    {
    }
}
