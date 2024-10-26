namespace Shopify.API.Models
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.Now;
		public DateTime? UpdatedOn { get; set;} 
	}
}
