using CommerceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.DTOs.Cart
{
	public class CartDto
	{
		public int Id { get; set; }
		public List<CartItemDto> Items { get; set; } = new();
		public decimal TotalAmount => Items.Sum(x => x.TotalPrice);//buraya totalprice yazılacak sonrasında
		public int TotalitemCount => Items.Sum(x => x.Quantity);
	}
	public class CartItemDto
	{
		public int Id { get; set; }
		public int  ProductId { get; set; }
		public string  ProductName { get; set; }=string.Empty;
		public string? ThumbnailUrl { get; set; }
		public decimal  UnitPrice { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice => UnitPrice * Quantity;
	}
	public class AddToCart
	{
		//Dto: Data Transfer Object.
		public int ProductId { get; set; }
		public int Quantity { get; set; }

	}
	public class UpdateCartItemDto
	{
		public int Quantity { get; set; }

	}

}
