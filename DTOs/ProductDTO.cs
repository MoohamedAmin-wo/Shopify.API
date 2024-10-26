using System.ComponentModel.DataAnnotations;

namespace Shopify.API.DTOs
{
	public record ProductDTO
	{
		[Required , MaxLength(200)]
		public string Name { get; set; }
		[Required , MaxLength(7000)]
		public string Description { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public IFormFile? Photos { get; set; }
		public int CategoryId { get; set; }
	}
}
