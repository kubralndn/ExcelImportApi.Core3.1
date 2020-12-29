using BUSINESS_LOGIC.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC.Services
{
    public class BaseService<TEntity> : IBase<TEntity> where TEntity : class
	{
		private readonly IRepository<TEntity> _repository;
		public BaseService(IRepository<TEntity> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public virtual async Task InsertUsingSqlBulkCopy(List<TEntity> entities)
		{

			using var transaction = await _repository.BeginTransactionAsync();
			var parameters = typeof(TEntity)
				.GetProperties()		
				.Select(x => x.Name)
				.ToArray();

			try
			{
				await _repository.InsertUsingSqlBulkCopy(entities, parameters);

				await transaction.CommitAsync();
			}
			catch (Exception)
			{
				await transaction.RollbackAsync();
			}
		}
	}
}
