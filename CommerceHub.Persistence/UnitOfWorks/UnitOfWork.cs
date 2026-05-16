using CommerceHub.Domain.Entities;
using CommerceHub.Persistence.Context;
using CommerceHub.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.UnitOfWorks
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;

		private IRepository<User>? _users;
		private IRepository<Product>? _products;
		private IRepository<Category>? _categories;
		private IRepository<Order>? _orders;
		private IRepository<OrderItem>? _orderItems;
		private IRepository<Cart>? _carts;
		private IRepository<CartItem>? _cartItems;
		private IRepository<ProductImage>? _productImages;
		private IRepository<Payment>? _payments;
		private IRepository<Address>? _addresses;
		private IRepository<EmailLog>? _emailLogs;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}
		public IRepository<User> Users => _users ??= new Repositories<User>(_context);

		public IRepository<Product> Products => _products ??= new Repositories<Product>(_context);

		public IRepository<Category> Categories => _categories ??= new Repositories<Category>(_context);

		public IRepository<ProductImage> ProductImages => _productImages ??= new Repositories<ProductImage>(_context);

		public IRepository<Cart> Carts => _carts ??= new Repositories<Cart>(_context);

		public IRepository<Order> Orders => _orders??= new Repositories<Order>(_context);

		public IRepository<CartItem> CartItems =>_cartItems ??=new Repositories<CartItem>(_context);

		public IRepository<OrderItem> OrderItems => _orderItems??=new Repositories<OrderItem>(_context);
		public IRepository<Payment> Payments => _payments ??= new Repositories<Payment>(_context);

		public IRepository<Address> Address => _addresses ??= new Repositories<Address>(_context);

		public IRepository<EmailLog> EmailLogs => _emailLogs??=new Repositories<EmailLog>(_context);

		public void Dispose()=>_context.Dispose();
		public async Task<int> SaveChangesAsync()=> await _context.SaveChangesAsync();
		
		
	}
}
