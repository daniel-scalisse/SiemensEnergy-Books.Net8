using Books_Business.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books_Business.Modules.Genders
{
    public interface IGenderRepository : IRepository<Gender>
    {
        Task<IEnumerable<KeyValuePair<int, string>>> GetSummary();
    }
}