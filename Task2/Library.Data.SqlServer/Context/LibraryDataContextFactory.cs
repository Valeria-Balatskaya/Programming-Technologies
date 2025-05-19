namespace Library.Data.SqlServer.Context
{
    public class LibraryDataContextFactory
    {
        private readonly string _connectionString;

        public LibraryDataContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ILibraryDataContext CreateContext()
        {
            return new LibraryDataContext(_connectionString);
        }
    }
}