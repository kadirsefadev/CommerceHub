using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.DTOs.Category
{
	public class CategoryDto
	{

		public int Id{ get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		public int ProductCount { get; set; }


	}
	public class CategoryCreateDto
	{
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
	}
	public class CategoryUpdateDto
	{
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
	}
}
