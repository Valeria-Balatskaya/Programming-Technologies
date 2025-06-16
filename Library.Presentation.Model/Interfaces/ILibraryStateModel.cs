

namespace Library.Presentation.Model.Interfaces
{
    public interface ILibraryStateModel
    {
        public IEnumerable<IBookModel> Books { get; }


        public IEnumerable<IUserModel> Users { get; }
    }
}
