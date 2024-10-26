using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.API.Abstract;
using Shopify.API.DTOs;
using Shopify.API.Helpers;
using Shopify.API.Models;

namespace Shopify.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpPost("CreateAsync")]
		public async Task<IActionResult> Create([FromForm] ProductDTO dto)
		{
			if (dto.Photos is null)
				return BadRequest("Photo is Required !");
			if (dto.Photos.Length > AppConsts.MaxAllowedSize)
				return BadRequest("The Max allowed size is 2MB");
			if (!AppConsts.AllowedExtenssions.Contains(Path.GetExtension(dto.Photos.FileName)))
				return BadRequest("The allowed extenssions is [jpg , png]");

			using var dataStream = new MemoryStream();
			await dto.Photos.CopyToAsync(dataStream);

			var product = _mapper.Map<Product>(dto);
			product.Photos = dataStream.ToArray();

			await _unitOfWork.Products.CreateAsync(product);
			_unitOfWork.Commit();

			return Ok(_mapper.Map<ProductDetailsDTO>(product));
		}
		[HttpPost("ToggleStatusAsync")]
		public async Task<IActionResult> ToggleStatus(int id)
		{
			var product = await _unitOfWork.Products.FindByIdAsync(id);
			if (product is null)
				return NotFound($"Couldn't find product with this Id {id}");

			product.IsDeleted = !product.IsDeleted;
			product.UpdatedOn = DateTime.Now;

			_unitOfWork.Commit();

			return Ok($"{_mapper.Map<ProductDetailsDTO>(product).Name} has been updated ");
		}

		[HttpPut("UpdateAsync")]
		public async Task<IActionResult> Update(int id, [FromForm] ProductDTO dto)
		{
			var product = await _unitOfWork.Products.FindByIdAsync(id);
			if (product is null)
				return NotFound($"Couldn't find product with this Id {id}");

			if (dto.Photos != null)
			{
				if (dto.Photos.Length > AppConsts.MaxAllowedSize)
					return BadRequest("The Max allowed size is 2MB");
				if (!AppConsts.AllowedExtenssions.Contains(Path.GetExtension(dto.Photos.FileName)))
					return BadRequest("The allowed extenssions is [jpg , png]");

				using var dataStream = new MemoryStream();
				await dto.Photos.CopyToAsync(dataStream);

				_unitOfWork.Commit();
			}
			product = _mapper.Map<Product>(dto);
			product.Photos = product.Photos;
			product.UpdatedOn = DateTime.Now;

			await _unitOfWork.Products.UpdateAsync(product);
			_unitOfWork.Commit();

			return Ok(_mapper.Map<ProductDetailsDTO>(product));
		}

		[HttpDelete("DeleteAsync")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var product = await _unitOfWork.Products.FindByIdAsync(id);
			if (product is null)
				return NotFound($"Couldn't find product with this Id {id}");

			await _unitOfWork.Products.DeleteAsync(product);
			_unitOfWork.Commit();
			return Ok($"{_mapper.Map<ProductDetailsDTO>(product).Name} has been deleted Successfully ");
		}

		[HttpGet("FindByIdAsync")]
		public async Task<IActionResult> FindById(int id)
		{
			var product = await _unitOfWork.Products.FindByIdAsync(id);
			if (product is null)
				return NotFound($"Couldn't find product with this Id {id}");

			if (product.IsDeleted == true)
				return BadRequest("This product is disActive !");

			return Ok(_mapper.Map<ProductDetailsDTO>(product));
		}

		[HttpGet("FindByKeyWordAsync")]
		public async Task<IActionResult> FindByWord(string keyword)
		{
			var products = await _unitOfWork.Products.FindByCreatriaAsync(predicate: x => x.Name.Contains(keyword), stopTracking: true);
			if (products is null)
				return NotFound($"Couldn't find product with this Keyword {keyword}");

			return Ok(_mapper.Map<IReadOnlyCollection<ProductDetailsDTO>>(products));
		}
		[HttpGet("GetAllAsync")]
		public async Task<IActionResult> GetAll()
		{
			var products = await _unitOfWork.Products.FindByCreatriaAsync(
				predicate: x => x.IsDeleted == false,
				stopTracking: true);

			if (products is null)
				return NotFound("No Products were added yet !");

			return Ok(_mapper.Map<IReadOnlyCollection<ProductDetailsDTO>>(products));
		}


		//[HttpGet("GetByCategoryAsync")]
		//public async Task<IActionResult> GetByCategory(string keyword)
		//{
		//	var category = await _unitOfWork.categories.FindByCreatriaAsync(predicate : x=>x.Name.Equals(keyword));
		//	if (category is null)
		//		return NotFound($"No Category with this keyword [{keyword}] was found ");

		//	var products = await _unitOfWork.Products.GetAsync(
		//		predicate : x=>x.CategoryId == category.Id ,
		//		stopTracking : true,
		//		orderByDirection : "ASC");
		//	if (products is null)
		//		return NotFound($"{keyword} has no products !");

		//	return Ok(_mapper.Map<IReadOnlyCollection<ProductDetailsDTO>>(products));
		//}
	}
}
