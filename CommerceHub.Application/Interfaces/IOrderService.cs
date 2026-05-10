using CommerceHub.Application.DTOs.Order;
using CommerceHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDetailDto> CreateOrderAsync(int userId, CreateOrderDto createOrderDto);
        Task<List<OrderListDto>>GetMyOrdersAsync(int userId);
        Task<OrderDetailDto> GetOrderByIdAsync(int userId, int orderId,bool isAdmin);
        Task<List<OrderListDto>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);




    }
}
