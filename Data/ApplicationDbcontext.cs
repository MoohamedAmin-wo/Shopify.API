using Shopify.API.Data.Configurations;

namespace Shopify.API.Data
{
	public class ApplicationDbcontext : DbContext
	{
		public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfigurations).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfigurations).Assembly);
		}
	}
}
