using Shopify.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Shopify.API.DTOs
{
	public record CategoryDTO
	{
		[Required , MaxLength(200)]
		public string Name { get; set; }
		[Required , MaxLength(700)]
		public string Description { get; set; }
	}
}
