using Books_Business.Core.Models;
using Books_Business.Modules.Authors;
using Books_Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Data.Modules.Authors
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(BooksDbContext context) : base(context)
        {

        }

        public override async Task<PagedResult<Author>> GetPaged(int pageSize, int page, string query)
        {
            int total;
            IEnumerable<Author> list;
            if (!string.IsNullOrWhiteSpace(query))
            {
                total = await Db.Authores.AsNoTracking()
                    .Where(a => a.Name.Contains(query))
                    .CountAsync();

                list = await Db.Authores.AsNoTracking()
                    .Where(a => a.Name.Contains(query))
                    .OrderBy(a => a.Name)
                    .Skip(pageSize * (page - 1)).Take(pageSize)
                    .ToListAsync();
            }
            else
            {
                total = await Db.Authores.AsNoTracking().CountAsync();

                list = await Db.Authores.AsNoTracking()
                    .OrderBy(a => a.Name)
                    .Skip(pageSize * (page - 1)).Take(pageSize)
                    .ToListAsync();
            }

            return new PagedResult<Author>
            {
                List = list,
                Total = total,
                Page = page,
                Size = pageSize
            };
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetSummary()
        {
            return await Db.Authores.AsNoTracking()
                .Select(a => new KeyValuePair<int, string>(a.Id, a.Name))
                .ToListAsync();
        }
    }
}