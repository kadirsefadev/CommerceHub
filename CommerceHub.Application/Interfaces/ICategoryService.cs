using CommerceHub.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Interfaces
{
	public interface ICategoryService
	{
		Task<List<CategoryDto>> GetAllAsync();
		Task<CategoryDto> GetByIdAsync(int id);
		Task<CategoryDto> CreateAsync(CategoryCreateDto
			categoryCreateDto);
		Task<CategoryDto> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
		Task<CategoryDto> DeleteAsync(int id);

	}
}
