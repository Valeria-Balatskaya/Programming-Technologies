namespace Library.Data.Interfaces
{
    public interface IDataRepository
    {
        IUserRepository Users { get; }
        ICatalogRepository Catalog { get; }
        IStateRepository State { get; }
        IEventRepository Events { get; }
    }
}
