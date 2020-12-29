using DAL.Interfaces;
using FastMember;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository<TEntity> :  IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly string CONNECTION_STRING = string.Empty;

        private DbSet<TEntity> _set;

        public Repository(ExcelImportDbContext context, IConfiguration config)
        {
            _context = context;
            CONNECTION_STRING = config.GetConnectionString("Default");
        }

        protected DbSet<TEntity> Set
        {
            get { return _set ?? (_set = _context.Set<TEntity>()); }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task InsertUsingSqlBulkCopy(List<TEntity> entities, string[] parameters)
        {           
            using (var sqlCopy = new SqlBulkCopy(CONNECTION_STRING))
            {
                sqlCopy.DestinationTableName = $"{typeof(TEntity).Name}s";
                sqlCopy.BatchSize = entities.Count;
                using var reader = ObjectReader.Create(entities, parameters);
                sqlCopy.WriteToServer(reader);
            }

            await Task.FromResult(true);
        }
    }
}
