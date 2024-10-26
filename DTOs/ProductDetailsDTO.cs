namespace Shopify.API.DTOs
{
	public record ProductDetailsDTO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public int CategoryId { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
