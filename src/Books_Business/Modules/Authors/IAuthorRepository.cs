using Books_Business.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books_Business.Modules.Authors
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<KeyValuePair<int, string>>> GetSummary();
    }
}