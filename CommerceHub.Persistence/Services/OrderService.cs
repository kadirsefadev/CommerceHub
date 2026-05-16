using CommerceHub.Application.DTOs.Order;
using CommerceHub.Application.Exceptions;
using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Domain.Enums;
using CommerceHub.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public OrderService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

  
        public async Task<OrderDetailDto> CreateOrderAsync(int userId, CreateOrderDto createOrderDto)
        {
          var cart = await _unitOfWork.Carts.Query()
                .Include(x=>x.Items).ThenInclude(x=>x.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if(cart == null || !cart.Items.Any())
                throw new AppException("Sepetinizde ürün bulunmamaktadır.");
            foreach (var item in cart.Items)
            {
                if (item.Quantity > item.Product.StockQuantity)
                    // benim sepetimde 2 urun ama stokta 1 urun 
                    throw new AppException($"Ürün stokta yeterli miktarda bulunmamaktadır: {item.Product.Name}");
            }
            var order = new Order
            {
                UserId = userId,
                OrderNumber = GenerateOrderNumber(),
                OrderStatus = OrderStatus.Pending,
                ShippingFullName = createOrderDto.ShippingFullName,
                ShippingPhone = createOrderDto.ShippingPhone,
                ShippingCity = createOrderDto.ShippingCity,
                ShippingDistrict = createOrderDto.ShippingDistrict,
                ShippingFullAddress = createOrderDto.ShippingFullAddress,
                Items = cart.Items.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    UnitPrice = x.Product.Price
                }).ToList()

            };
            order.TotalAmount= order.Items.Sum(x => x.Quantity * x.UnitPrice);
            foreach (var item in cart.Items)
            {
                item.Product.StockQuantity -= item.Quantity;
                _unitOfWork.Products.Update(item.Product);
            }
            await _unitOfWork.Orders.AddAsync(order);

            foreach (var item in cart.Items)
            {
                _unitOfWork.CartItems.Remove(item);
            }
            await _unitOfWork.SaveChangesAsync();

            var user=await _unitOfWork.Users.GetByIdAsync(userId);
            if (user!=null)
                await _emailService.SendAsync(user.Email, $"Şiparişiniz alındı :#{order.OrderNumber}", $"Toplam :{order.TotalAmount:C}");
                return await GetOrderByIdAsync(order.Id, userId, isAdmin:true );




        }

        public async Task<List<OrderListDto>> GetAllOrdersAsync()
        {
            return await _unitOfWork.Orders.Query()
                .Include(x=>x.Items)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new OrderListDto
                {
                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    TotalAmount = x.TotalAmount,
                    OrderStatus = x.OrderStatus,
                    CreatedAt = x.CreatedAt,
                    ItemCount = x.Items.Count
                }).ToListAsync();

        }

        public async Task<List<OrderListDto>> GetMyOrdersAsync(int userId)
        {
            return await _unitOfWork.Orders.Query()
              .Include(x => x.Items)
              .Where(x => x.UserId == userId)
              .OrderByDescending(x => x.CreatedAt)
              .Select(x => new OrderListDto
              {
                  Id = x.Id,
                  OrderNumber = x.OrderNumber,
                  TotalAmount = x.TotalAmount,
                  OrderStatus = x.OrderStatus,
                  CreatedAt = x.CreatedAt,
                  ItemCount = x.Items.Count
              }).ToListAsync();
        }

        public async Task<OrderDetailDto> GetOrderByIdAsync(int userId, int orderId, bool isAdmin)
        {
            var query = _unitOfWork.Orders.Query()
                         .Include(x => x.Items).ThenInclude(x => x.Product)
                         .Include(x=>x.Payment)
                         .Where(x => x.Id == orderId);
            if (!isAdmin)
                query = query.Where(x => x.UserId == userId);
            var order =await query.FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Sipariş bulunamadı ID: {orderId}");
            return MapToDetailDto(order);
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order= await _unitOfWork.Orders.GetByIdAsync(orderId)
                ?? throw new NotFoundException($"Sipariş bulunamadı ID: {orderId}");
            order.OrderStatus = newStatus;
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
        public static string GenerateOrderNumber()
        {
            var date= DateTime.UtcNow.ToString("yyyyMMdd");
            var random = Random.Shared.Next(1000, 9999);
            return $"ORD-{date}-{random}";
        }
        private static OrderDetailDto MapToDetailDto(Order o) => new()
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            TotalAmount = o.TotalAmount,
            OrderStatus = o.OrderStatus,
            CreatedAt = o.CreatedAt,
            ShippingFullName = o.ShippingFullName,
            ShippingPhone = o.ShippingPhone,
            ShippingCity = o.ShippingCity,
            ShippingDistrict = o.ShippingDistrict,
            ShippingFullAddress = o.ShippingFullAddress,
            Items = o.Items.Select(i => new OrderItemDto
            {
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                TotalPrice = i.UnitPrice * i.Quantity
            }).ToList(),

            Payment = o.Payment == null ? null : new PaymentInfoDto
            {
                Amount = o.Payment.Amount,
                Status = o.Payment.Status.ToString(),
                CardLastFour = o.Payment.CardLastFour,
                PaidAt = o.Payment.PaidAt

            }
        };

    }
}
