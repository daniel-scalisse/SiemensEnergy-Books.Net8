using Books_Business.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Books_Business.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<PagedResult<TEntity>> GetPaged(int pageSize, int page, string query);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> filter);
        Task<int> Count(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(int id);
        Task<int> SaveChanges();
    }
}