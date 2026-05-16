using CommerceHub.Domain.Entities;
using CommerceHub.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        /*
  Unit of work: birden fazla database işlemini tek bir iş paketinde / transaction
      gibi yonetme mantığıdır.

  Unitofwork uygulama icinde yapılan veri değişiklerini takip eden ve bu değişikleri
      tek bir noktadan commit veya rollback eden tasarımdır.

        sipariş oluştu
        stokdüşücek
        odeme kaydı olusacak

        ya hepsi basarili olsun yada hiç biri olmayacak

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
