using BUSINESS_LOGIC.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC.Services
{
	public class ColorService : BaseService<Color>, IColor
	{
		private readonly IRepository<Color> _repository;
		public ColorService(IRepository<Color> repository) : base(repository)
		{
			_repository = repository;
		}
		public override async Task InsertUsingSqlBulkCopy(List<Color> entities)
		{
			using var transaction = await _repository.BeginTransactionAsync();

			string[] parameters = new[]
			{
				nameof(Color.CreatedOn),
				nameof(Color.Name)
			};

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