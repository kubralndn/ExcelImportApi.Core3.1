using DAL.Configurations;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ExcelImportDbContext : DbContext
    {

        public ExcelImportDbContext(DbContextOptions<ExcelImportDbContext> options) : base(options) { }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ColorConfiguration());
            builder.ApplyConfiguration(new ArticleConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
