using Books_Business.Core.Models;
using Books_Business.Modules.Books;
using Books_Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Books_Data.Modules.Books
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BooksDbContext context) : base(context)
        {

        }

        public override async Task<PagedResult<Book>> GetPaged(int pageSize, int page, string query)
        {
            int total;
            IEnumerable<Book> list;
            if (!string.IsNullOrWhiteSpace(query))
            {
                total = await Db.Books.AsNoTracking()
                    .Where(
                        l => l.Title.Contains(query) ||
                        l.SubTitle.Contains(query) ||
                        l.Author.Name.Contains(query) ||
                        l.Gender.Name.Contains(query)
                        )
                    .CountAsync();

                list = await Db.Books.AsNoTracking()
                    .Include(l => l.Author)
                    .Include(l => l.Gender)
                    .Include(l => l.BookImage)
                    .Where(
                        l => l.Title.Contains(query) ||
                        l.SubTitle.Contains(query) ||
                        l.Author.Name.Contains(query) ||
                        l.Gender.Name.Contains(query)
                        )
                    .OrderBy(l => l.Title)
                    .Skip(pageSize * (page - 1)).Take(pageSize)
                    .ToListAsync();
            }
            else
            {
                total = await Db.Books.AsNoTracking().CountAsync();

                list = await Db.Books.AsNoTracking()
                    .Include(l => l.Author)
                    .Include(l => l.Gender)
                    .Include(l => l.BookImage)
                    .OrderBy(l => l.Title)
                    .Skip(pageSize * (page - 1)).Take(pageSize)
                    .ToListAsync();
            }

            return new PagedResult<Book>
            {
                List = list,
                Total = total,
                Page = page,
                Size = pageSize
            };
        }

        public async Task<Book> GetDetailsById(int id)
        {
            return await Db.Books.AsNoTracking()
                .Include(l => l.Gender)
                .Include(l => l.Author)
                .Include(l => l.BookImage)
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<Book>> Search(Expression<Func<Book, bool>> filter)
        {
            return await DbSet.AsNoTracking()
                .Include(l => l.Gender)
                .Include(l => l.Author)
                .Where(filter)
                .OrderBy(l => l.Title)
                .ToListAsync();
        }

        public override async Task Add(Book entity)
        {
            await base.Add(entity);
        }

        public override async Task Update(Book entity)
        {
            await base.Update(entity);

            if (entity.BookImage != null)
            {
                DbSet<BookImage> dbSetImg = Db.Set<BookImage>();
                if (Db.BookImages.AsNoTracking().Where(l => l.Id == entity.Id).Count() == 0)
                    dbSetImg.Add(entity.BookImage);
                else
                    Db.Entry(entity.BookImage).State = EntityState.Modified;

                await SaveChanges();
            }
        }

        public override async Task Remove(int id)
        {
            Db.Entry(new BookImage { Id = id }).State = EntityState.Deleted;

            await base.Remove(id);
        }
    }
}