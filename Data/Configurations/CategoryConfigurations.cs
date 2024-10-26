using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.API.Models;

namespace Shopify.API.Data.Configurations
{
	public class CategoryConfigurations : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Categories" , "A001");
			builder.Property(p => p.Name).HasMaxLength(200).IsRequired() ;
			builder.Property(p => p.Description).HasMaxLength(700).IsRequired();
			builder.Property(p => p.IsDeleted).HasDefaultValue(false);
		}
	}
}
