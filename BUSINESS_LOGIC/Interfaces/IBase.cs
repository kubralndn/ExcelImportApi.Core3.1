using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC.Interfaces
{
    public interface IBase<TEntity> where TEntity : class
	{
		
		Task InsertUsingSqlBulkCopy(List<TEntity> entities);
		Task<IEnumerable<TEntity>> GetAllAsync();
	}
}
