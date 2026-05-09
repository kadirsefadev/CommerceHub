using CommerceHub.Application.DTOs.Category;
using CommerceHub.Application.DTOs.Product;
using CommerceHub.Application.Exceptions;
using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Persistence.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto)
		{
			var category = new Category
			{
				Name = categoryCreateDto.Name,
				Description = categoryCreateDto.Description,
				ImageUrl = categoryCreateDto.ImageUrl,
			};
			await _unitOfWork.Categories.AddAsync(category);
			await _unitOfWork.SaveChangesAsync();
			return await GetByIdAsync(category.Id);
		}

		public Task<CategoryDto> DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<CategoryDto>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<CategoryDto> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<CategoryDto> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
		{
			throw new NotImplementedException();
		}
	}
}
