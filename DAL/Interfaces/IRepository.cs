using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task InsertUsingSqlBulkCopy(List<TEntity> entities, string[] parameters);
    }
}