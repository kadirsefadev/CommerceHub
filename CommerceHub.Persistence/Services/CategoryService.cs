using CommerceHub.Application.DTOs.Category;
using CommerceHub.Application.DTOs.Product;
using CommerceHub.Application.Exceptions;
using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;


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

		public async Task DeleteAsync(int id)
		{
			var category = await _unitOfWork.Categories.GetByIdAsync(id)
		   ?? throw new NotFoundException($"Kategori  bulunamadı ID: {id}");
			category.IsDeleted = true;
			category.UpdatedAt = DateTime.UtcNow;
			_unitOfWork.Categories.Update(category);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<List<CategoryDto>> GetAllAsync()
		{
			return await _unitOfWork.Categories.Query()
				.Include(x=>x.Products)
				.Select(x=> new CategoryDto
				{
					Id = x.Id,
					Name = x.Name,
					Description = x.Description,
					ImageUrl = x.ImageUrl,
					ProductCount = x.Products.Count(p => p.IsActive)
				}).ToListAsync();
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

		public async Task<CategoryDto> UpdateAsync(int id,CategoryUpdateDto categoryUpdateDto)
		{
			
			var category =await _unitOfWork.Categories.GetByIdAsync(id)
				?? throw new NotFoundException($"Kategori Bulunamadı ID:{id}");
			category.Name = categoryUpdateDto.Name;
			category.Description = categoryUpdateDto.Description;
			category.ImageUrl = categoryUpdateDto.ImageUrl;
			category.UpdatedAt = DateTime.UtcNow;

			_unitOfWork.Categories.Update(category);
			await _unitOfWork.SaveChangesAsync();
			return await GetByIdAsync(id);


		}
	}
}
