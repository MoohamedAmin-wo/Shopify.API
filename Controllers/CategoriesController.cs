using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.API.Abstract;
using Shopify.API.DTOs;
using Shopify.API.Models;

namespace Shopify.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpPost("CreateAsync")]
		public async Task<IActionResult> Create([FromForm]CategoryDTO dto)
		{
			var category = _mapper.Map<Category>(dto);
			await _unitOfWork.categories.CreateAsync(category);
			_unitOfWork.Commit();

			return Ok(_mapper.Map<CategoryDetailsDTO>(category));
		}

		[HttpPost("ToggleStatusAsync")]
		public async Task<IActionResult> ToggleStatus(int id)
		{
			var category = await _unitOfWork.categories.FindByIdAsync(id);
			if (category is null)
				return NotFound($"Couldn't find Category with Id {id}!");

			category.IsDeleted = !category.IsDeleted;
			category.UpdatedOn = DateTime.Now;

			_unitOfWork.Commit();
			return Ok($"{_mapper.Map<CategoryDetailsDTO>(category).Name} has been updated");
		}

		[HttpPut("UpdateAsync")]
		public async Task<IActionResult> Update(int id , [FromForm] CategoryDTO dto)
		{
			var category = await _unitOfWork.categories.FindByIdAsync(id);
			if (category is null)
				return NotFound($"Couldn't find Category with Id {id}!");

			category = _mapper.Map<Category>(dto);
			category.UpdatedOn = DateTime.Now;

			await _unitOfWork.categories.UpdateAsync(category);
			_unitOfWork.Commit();

			return Ok(_mapper.Map<CategoryDetailsDTO>(category));
		}
	}
}
