using CommerceHub.Application.DTOs.Category;
using CommerceHub.Application.DTOs.Product;
using CommerceHub.Application.Exceptions;
using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
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

		public async Task<CategoryDto> DeleteAsync(int id)
		{
			var category = await _unitOfWork.Categories.GetByIdAsync(id)
		   ?? throw new NotFoundException($"Kategori  bulunamadı ID: {id}");
			category.IsDeleted = true;
			category.UpdatedAt = DateTime.UtcNow;
			_unitOfWork.Categories.Update(category);
			await _unitOfWork.SaveChangesAsync();
		}

		public Task<List<CategoryDto>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<CategoryDto> GetByIdAsync(int id)
		{
			var category = await _unitOfWork.Categories.Query()
			.Include(x => x.Products)
			.FirstOrDefaultAsync(x => x.Id == id)
			?? throw new NotFoundException($"Kategori Bulunamadı ID:{id}");

			return new CategoryDto
			{
				Id = id,
				Name = category.Name,
				Description = category.Description,
				ImageUrl = category.ImageUrl,
				ProductCount=category.Products.Count(x=>x.IsActive)
			};
		}

		public Task<CategoryDto> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
		{
			throw new NotImplementedException();
		}
	}
}
