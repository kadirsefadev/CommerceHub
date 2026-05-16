using CommerceHub.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int userId);
        Task<CartDto>AddItemAsync(int userId, AddToCart cartDto);
        Task<CartDto> UpdateItemAsync(int userId,int cartItemId, UpdateCartItemDto updateCartItemDto);
        Task RemoveItemAsync(int userId, int cartItemId);
        Task ClearCartAsync(int userId);

    }
}
