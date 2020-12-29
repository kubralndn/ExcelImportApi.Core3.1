using BUSINESS_LOGIC.Dtos;
using BUSINESS_LOGIC.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC.Services
{
    public class ProductService : BaseService<Product>, IProduct
    {
        private readonly IColor _colorService;
        private readonly IArticle _articleService;
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> productRepository,
            IColor colorService,
            IArticle articleService) : base(productRepository)
        {
            _repository = productRepository;
            _colorService = colorService;
            _articleService = articleService;
        }

        public async Task<int> Import(List<ImportDataViewModel> importData, string file = "")
        {
            try
            {
                List<Article> newArticles = importData
                    .GroupBy(x => x.ArticleCode)
                    .Select(x => new Article()
                    {
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                        Code = x.First().ArticleCode,
                        ColorCode = x.First().ColorCode,
                        Description = x.First().Description
                    }).ToList();

                List<Color> newColors = importData
                    .GroupBy(x => x.Color)
                    .Select(x =>
                    new Color()
                    {
                        CreatedOn = DateTime.Now,
                        Name = x.First().Color
                    }).ToList();

                await _colorService.InsertUsingSqlBulkCopy(newColors);

                await _articleService.InsertUsingSqlBulkCopy(newArticles);

                var allArticles = (await _articleService.GetAllAsync()).ToList();

                var allColors = (await _colorService.GetAllAsync()).ToList();

                var newProducts = importData
                    .Select(x => new Product
                    {
                        ArticleId = allArticles.FirstOrDefault(a => a.Code == x.ArticleCode).Id,
                        ColorId = allColors.FirstOrDefault(c => c.Name == x.Color).Id,
                        CreatedOn = DateTime.Now,
                        DeliveredIn = x.DeliveredIn,
                        DiscountPrice = x.DiscountPrice,
                        Key = x.Key,
                        Price = x.Price,
                        Q1 = x.Q1,
                        Size = x.Size
                    }).ToList();

                await InsertUsingSqlBulkCopy(newProducts);

                return newProducts.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task InsertUsingSqlBulkCopy(List<Product> entities)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            string[] parameters = new[]
            {
                nameof(Product.Id),
                nameof(Product.ArticleId),
                nameof(Product.ColorId),
                nameof(Product.Key),
                nameof(Product.Price),
                nameof(Product.DiscountPrice),
                nameof(Product.DeliveredIn),
                nameof(Product.Q1),
                nameof(Product.Size),
                nameof(Product.CreatedOn)
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
