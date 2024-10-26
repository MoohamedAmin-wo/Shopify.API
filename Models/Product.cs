namespace Shopify.API.Models
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public double TotalSalesofProduct { get; set; }
		public byte[]? Photos { get; set; } 
		public int CategoryId { get;set; }	
		public Category Category { get; set; }
	}
}
