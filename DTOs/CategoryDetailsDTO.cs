namespace Shopify.API.DTOs
{
	public record CategoryDetailsDTO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
