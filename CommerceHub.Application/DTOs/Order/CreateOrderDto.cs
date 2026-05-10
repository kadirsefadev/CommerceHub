using CommerceHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.DTOs.Order
{
	public class CreateOrderDto
	{
		public string ShippingFullName { get; set; } = string.Empty;
		public string ShippingPhone { get; set; } = string.Empty;
		public string ShippingCity { get; set; } = string.Empty;
		public string ShippingDistrict { get; set; } = string.Empty;
		public string ShippingFullAddress { get; set; } = string.Empty;

	}
	public class OrderListDto
	{
		public int Id { get; set; }
		public string OrderNumber { get; set; }
		public decimal TotalAmount { get; set; }
		public OrderStatus OrderStatus { get; set; }
		public string StatusText => OrderStatus.ToString();
		public DateTime CreatedAt { get; set; }
		public int ItemCount { get; set; }
	}
	public class OrderDetailDto
	{
		public int Id { get; set; }
		public string OrderNumber { get; set; }
		public decimal TotalAmount { get; set; }
		public OrderStatus OrderStatus { get; set; }
		public string StatusText => OrderStatus.ToString();

		public DateTime CreatedAt { get; set; }

		public string ShippingFullName { get; set; } = string.Empty;
		public string ShippingPhone { get; set; } = string.Empty;
		public string ShippingCity { get; set; } = string.Empty;
		public string ShippingDistrict { get; set; } = string.Empty;
		public string ShippingFullAddress { get; set; } = string.Empty;
		public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
		public PaymentInfoDto? Payment { get; set; }
	}
	public class OrderItemDto
	{
		public string ProductName { get; set; } = string.Empty;
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }

	}
	public class PaymentInfoDto
	{

		public decimal Amount { get; set; }
		public string Status { get; set; } = string.Empty;
		public string? CardLastFour { get; set; }
		public DateTime? PaidAt { get; set; }

	}
	public class UpdateOrderStatusDto
	{
		public OrderStatus NewStatus { get; set; }
	}
}
