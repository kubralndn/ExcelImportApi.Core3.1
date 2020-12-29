using BUSINESS_LOGIC.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC.Services
{
    public class ArticleService : BaseService<Article>, IArticle
	{
		private readonly IRepository<Article> _repository;
		public ArticleService(IRepository<Article> repository) : base(repository)
		{
			_repository = repository;
		}
		public override async Task InsertUsingSqlBulkCopy(List<Article> entities)
		{
			using var transaction = await _repository.BeginTransactionAsync();

			string[] parameters = new[]
			{
				nameof(Article.CreatedOn),
				nameof(Article.Code),
				nameof(Article.ColorCode),
				nameof(Article.Description)
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