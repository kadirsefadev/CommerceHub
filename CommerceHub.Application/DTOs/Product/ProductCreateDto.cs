using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.DTOs.Product
{
	public class ProductCreateDto
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal  Price { get; set; }
		public int  StockQuantity { get; set; }
		public string? ThumbnailUrl { get; set; }
		public int  CategoryId { get; set; }
	}
	public class ProductUpdateDto
	{
		public string  Name { get; set; } = string.Empty;
		public string Description  { get; set; } = string.Empty;
		public decimal  Price { get; set; }
		public int StockQuantity { get; set; }
		public string? ThumbnailUrl { get; set; }
		public int CategoryId { get; set; }
		public bool IsActive { get; set; }
	}
	public class ProductListDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public int  StockQuantity { get; set; }
		public string? ThumbnailUrl { get; set; }
		public string CategoryName { get; set; } = string.Empty;
		public bool  IsActive { get; set; }

	}
	public class ProductDetailDto
	{
		public int Id { get; set; }
		public string  Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public int StockQuantity { get; set; }
		public string? ThumbnailUrl { get; set; }
		public bool IsActive { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; } = string.Empty;
		public List<string> ImagesUrl { get; set; } = new();
	}
}
