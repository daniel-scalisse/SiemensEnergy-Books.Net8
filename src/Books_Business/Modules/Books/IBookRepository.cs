using Books_Business.Core.Data;
using System.Threading.Tasks;

namespace Books_Business.Modules.Books
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetDetailsById(int id);
    }
}