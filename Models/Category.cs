using Microsoft.EntityFrameworkCore;

namespace Shopify.API.Models
{
	[Index(nameof(Name),IsUnique = true)]
	public class Category : BaseEntity
	{
		public string Name { get; set; } 
		public string Description { get; set; }

		public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
	}
}
