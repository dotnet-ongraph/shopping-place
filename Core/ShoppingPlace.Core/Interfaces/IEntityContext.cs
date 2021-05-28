namespace Core.Interfaces
{
    public interface IEntityContext
    {
        IReadEntityRepository ReadRepository { get; }

        IWriteEntityRepository WriteRepository { get; }
    }
    public enum ContextName
    {
        CatalogDb,
        FulfillmentDb,
        ConnStr
    }
}
