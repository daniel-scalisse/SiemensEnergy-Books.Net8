using Books_Business.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books_Business.Core.Services
{
    public interface IService<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Remove(int id);
    }
}