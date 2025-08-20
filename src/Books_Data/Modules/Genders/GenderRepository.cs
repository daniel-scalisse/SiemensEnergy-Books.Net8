using Books_Business.Core.Models;
using Books_Business.Modules.Genders;
using Books_Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Data.Modules.Genders
{
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        public GenderRepository(BooksDbContext context) : base(context)
        {

        }

        public override async Task<PagedResult<Gender>> GetPaged(int pageSize, int page, string query)
        {
            int total;
            IEnumerable<Gender> list;
            if (!string.IsNullOrWhiteSpace(query))
            {
                total = await Db.Genders.AsNoTracking()
                    .Where(a => a.Name.Contains(query))
                    .CountAsync();

                list = await Db.Genders.AsNoTracking()
                    .Where(a => a.Name.Contains(query))
                    .OrderBy(a => a.Name)
                    .Skip(pageSize * (page - 1)).Take(pageSize)
                    .ToListAsync();
            }
            else
            {
                total = await Db.Genders.AsNoTracking().CountAsync();

                list = await Db.Genders.AsNoTracking()
                    .OrderBy(a => a.Name)
                    .Skip(pageSize * (page - 1)).Take(pageSize)
                    .ToListAsync();
            }

            return new PagedResult<Gender>
            {
                List = list,
                Total = total,
                Page = page,
                Size = pageSize
            };
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetSummary()
        {
            return await Db.Genders.AsNoTracking()
                .Select(a => new KeyValuePair<int, string>(a.Id, a.Name))
                .ToListAsync();
        }
    }
}