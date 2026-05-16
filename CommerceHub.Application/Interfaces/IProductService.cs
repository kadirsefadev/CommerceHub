using CommerceHub.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Interfaces
{
	public interface IProductService
	{
		Task<List<ProductListDto>>GetAllAsync(int? categoryId = null);
		Task<ProductDetailDto>GetByIdAsync(int id);
		Task<ProductDetailDto>CreateAsync(ProductCreateDto productDetailDto);

		Task<ProductDetailDto>UpdateAsync(int id,ProductUpdateDto productDetailDto);
		Task DeleteAsync(int id);


	}
}
