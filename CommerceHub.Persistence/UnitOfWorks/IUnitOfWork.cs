using CommerceHub.Domain.Entities;
using CommerceHub.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.UnitOfWorks
{
	public interface IUnitOfWork:IDisposable
	{
		/*
		  Unit of work: birden fazla database işlemini tek bir iş paketinde / transactionb gibi yonetme mantıgıdır.

		Unitofwork uygulama icinde yapılan veri değişiklerini takip eden ve bu değişikleri tek bir noktadan commit veya rollback eden tasarımdır.
		

		Sipariş Olustu
		Stok Düşücek 
		Odeme Kaydı Olusacak
		 
		Ya hepsi basarılı olsun yada hic biri olmayacak.
		 
		 */


		IRepository<User> Users { get; }
		IRepository<Product> Products { get; }
		IRepository<Category> Categories { get; }
		IRepository<ProductImage> ProductImages { get; }
		IRepository<Cart> Carts { get; }
		IRepository<Order> Orders { get; }
		IRepository<CartItem> CartItems { get; }
		IRepository<OrderItem> OrderItems { get; }
		IRepository<Payment> Payments { get; }
		IRepository<Address> Address { get; }
		IRepository<EmailLog> EmailLogs { get; }
		Task<int> SaveChangesAsync();

	  



	}
}
