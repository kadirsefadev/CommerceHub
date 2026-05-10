using CommerceHub.Application.DTOs.Cart;
using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.Services
{
    //sepet
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)=> _unitOfWork = unitOfWork;


        public Task<CartDto> AddItemAsync(int userId, AddToCart cartDto)
        {
            throw new NotImplementedException();
        }

        public Task ClearCartAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> GetCartAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItemAsync(int userId, int cartItemId)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> UpdateItemAsync(int userId, int cartItemId, UpdateCartItemDto updateCartItemDto)
        {
            throw new NotImplementedException();
        }


        private async Task<Cart> GetOrCreateCartAsync(int userId)
        {

            var cart =await _unitOfWork.Carts.Query()
                .Include(x=>x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if(cart!=null) return cart;

            cart= new Cart{  UserId = userId };

            await _unitOfWork.Carts.AddAsync(cart);
            await _unitOfWork.SaveChangesAsync();
            return cart;

        }
        private static CartDto MapToCartDto(Cart cart) => new()
        {
            Id = cart.Id,
            Items = cart.Items.Select(x => new CartItemDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                ThumbnailUrl = x.Product.ThumbnailUrl,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity
            }).ToList()
        };


    }
}
