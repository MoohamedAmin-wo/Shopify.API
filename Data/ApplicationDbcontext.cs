using Microsoft.EntityFrameworkCore;

namespace Shopify.API.Data
{
	public class ApplicationDbcontext : DbContext
	{
		public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
		{
		}
	}
}
