using Books_Business.Core.Data;
using Books_Business.Core.Models;
using Books_Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Books_Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly BooksDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(BooksDbContext db)
        {
            //Db = new BooksDbContext();

            Db = db;
            DbSet = Db.Set<TEntity>();//Atalho para a entidade.
        }

        public abstract Task<PagedResult<TEntity>> GetPaged(int pageSize, int page, string query);

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filter)
        {
            return await DbSet.AsNoTracking().Where(filter).ToListAsync();
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).CountAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            //Db.Set<TEntity>().Add(entity); //Verbose.
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public virtual async Task Remove(int id)
        {
            //Como é uma entidade genérica, e todas têm um ID, não há necessidade de pesquisar no banco de dados.
            DbSet.Remove(await DbSet.FindAsync(id));

            //Não funciona porque precisa de uma entidade existente, que já foi observada.
            //DbSet.Remove(new TEntity { Id = id });

            //Não funcionou em nenhuma entidade.
            //Db.Entry(new TEntity { Id = id }).State = EntityState.Deleted;
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}