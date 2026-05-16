using CommerceHub.Application.DTOs.Cart;
using CommerceHub.Application.Exceptions;
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


        public async Task<CartDto> AddItemAsync(int userId, AddToCart cartDto)
        {
            var cart= await GetOrCreateCartAsync(userId);
            var product = await _unitOfWork.Products.GetByIdAsync(cartDto.ProductId)
            ?? throw new NotFoundException($"Product with ID {cartDto.ProductId} not found.");

            if(!product.IsActive)
                throw new AppException($"Product with ID {cartDto.ProductId} is not available.");
            if(product.StockQuantity<cartDto.Quantity)
                throw new AppException($"Only {product.StockQuantity} units of product with ID {cartDto.ProductId} are available.");

            var existingCartItem = cart.Items.FirstOrDefault(x => x.ProductId == cartDto.ProductId);

            if (existingCartItem != null)
                existingCartItem.Quantity += cartDto.Quantity;
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = cartDto.ProductId,
                    Quantity = cartDto.Quantity,
                    UnitPrice = product.Price
                };
                await _unitOfWork.CartItems.AddAsync(cartItem);
            }
              
            await _unitOfWork.SaveChangesAsync();
            return await GetCartAsync(userId);

        
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartAsync(userId);
            foreach (var item in cart.Items)
                _unitOfWork.CartItems.Remove(new CartItem { Id = item.Id });
            await _unitOfWork.SaveChangesAsync();




        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
         
            var cart = await GetOrCreateCartAsync(userId);
            return MapToCartDto(cart);
        }

        public async Task RemoveItemAsync(int userId, int cartItemId)
        {
           
            var cart = await GetOrCreateCartAsync(userId);
            var item = cart.Items.FirstOrDefault(x => x.Id == cartItemId)
                ?? throw new NotFoundException($"Cart item with ID {cartItemId} not found in user's cart.");
            _unitOfWork.CartItems.Remove(item);
            await _unitOfWork.SaveChangesAsync();

        }

        public  async Task<CartDto> UpdateItemAsync(int userId, int cartItemId, UpdateCartItemDto updateCartItemDto)
        {
            var cart =await GetOrCreateCartAsync(userId);
            var item= cart.Items.FirstOrDefault(x => x.Id == cartItemId)
                ?? throw new NotFoundException($"Cart item with ID {cartItemId} not found in user's cart.");
            if(updateCartItemDto.Quantity <= 0)
                _unitOfWork.CartItems.Remove(item);
            else
            {
                item.Quantity = updateCartItemDto.Quantity;
                _unitOfWork.CartItems.Update(item);
            }
            await _unitOfWork.SaveChangesAsync();
            return await GetCartAsync(userId);
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
