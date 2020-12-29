using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
	{
		// separation of configuration classes bring us cleaner code, thats why i didn't seperate them in one generic class
		// used in ExcelImportDbContext OnModelCreating method 
		public void Configure(EntityTypeBuilder<Color> builder)
		{
			builder.HasKey(x => x.Id);

			builder
				.Property(m => m.Id)
				.UseIdentityColumn();

			builder.Property(x => x.CreatedOn).HasDefaultValueSql("getdate()");
		}
	}
}
