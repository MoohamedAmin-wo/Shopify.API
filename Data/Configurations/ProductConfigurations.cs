using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.API.Models;

namespace Shopify.API.Data.Configurations
{
	public class ProductConfigurations : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Product", "A001");
			builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
			builder.Property(p => p.Description).HasMaxLength(7000).IsRequired();
			builder.Property(p => p.IsDeleted).HasDefaultValue(false);
		}
	}
}
