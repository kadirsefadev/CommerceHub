using CommerceHub.Domain.Entities;
using CommerceHub.Domain.Enums;
using CommerceHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();
            await SeedCategoriesAsync(context);
            await SeedAdminUserAsync(context);
            await SeedProductAsync(context);
        }
        private static async Task SeedCategoriesAsync(AppDbContext context)
        {
            if (await context.Categories.AnyAsync()) return;
            var categories = new List<Category>
        {
            new Category(){ Name="Elektronik", Description="Telefon Bilgisayar aksesuar"},
            new Category(){ Name="Giyim", Description="Erkek,Kadın ve Cocuk giyim"},
            new Category(){ Name="Kitap", Description="Roman,Bilim ve Tarih"},
            new Category(){ Name="Ev&Yaşam", Description="Mobilya dekorasyon mutfak"},
            new Category(){ Name="Spor", Description="Spor malzemeleri ve ekipmanları"}
        };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        private static async Task SeedAdminUserAsync(AppDbContext context)
        {
            if (await context.Users.AnyAsync(x => x.Email == "admin@test.com")) return;
            var admin = new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@test.com",
                PasswordHash = "", //sonra yazılacaktır.
                Role = UserRoles.Admin,
                IsActive = true
            };
            await context.Users.AddRangeAsync(admin);
            await context.SaveChangesAsync();
        }
        private static async Task SeedProductAsync(AppDbContext context)
        {
            if (await context.Products.AnyAsync()) return;
           
            var electronicID = (await context.Categories.FirstOrDefaultAsync(x => x.Name == "Elektronik"))?.Id ?? 0;
            var bookID = (await context.Categories.FirstOrDefaultAsync(x => x.Name == "Kitap"))?.Id ?? 0;

            var products = new List<Product>
    {
         new(){Name="Apple Iphone 15 128GB", Price=48500, StockQuantity=50, CategoryId=electronicID, ThumnailUrl="https://cdn.vatanbilgisayar.com/Upload/PRODUCT/apple/thumb/0005-mtp03tu-a_small.jpg"},
         new(){Name="Samsung Galaxy S24 256GB", Price=38000, StockQuantity=125, CategoryId=electronicID, ThumnailUrl="https://cdn.vatanbilgisayar.com/Upload/PRODUCT/samsung/thumb/143389-7-1_small.jpg"},
         new(){Name="Apple Macbook Air M3 8 GB",Price=49639,StockQuantity=5,CategoryId=electronicID,ThumnailUrl="https://cdn.vatanbilgisayar.com/Upload/PRODUCT/apple/thumb/mxct3tu-a_small.jpg"},
         new(){Name="HarryPotter ve Felsefe Taşı",Description="Harry potter felsefe taşı baglangıc serisi ",StockQuantity=75,Price=500,CategoryId=bookID,ThumnailUrl="https://static.periplus.com/kFndxWov6OJOb0LxbIFBOf7GVFMKPgdSXTGWY9Cfex3Wdr1a.GQtmsE1NXLuc.mpg--"},


         new(){Name="HarryPotter Zümrüt anka yoldaşlıgı",Description="Finalden önce son bölüm",Price=1000,StockQuantity=5,CategoryId=bookID,ThumnailUrl="https://target.scene7.com/is/image/Target/GUEST_1c7dc567-7645-4c95-844f-9ab0457f29f1?wid=300&hei=300&fmt=pjpeg%22%7D" },
                 new(){Name="Pembe İnce Askılı İşlemeli Mini Elbise\r\n",Description="Romantik pembe tonunda, gövdesi zarif işlemeli ve ince askılı kokteyl elbisesi. A-kesim etek formuyla mezuniyet ve partiler için ideal.\r\n\r\n",StockQuantity=50,Price=15000,ThumnailUrl="https://reinamaison.com/wp-content/uploads/2025/10/beaded-short-dress-thin-straps-aline-2-300x300.webp%22%7D"},

            new(){Name="Alyce Maxi Dress ",Description="Mandarin Collar Button Down with Detachable Waist Tie in Chocolate",StockQuantity=150,Price=50000,ThumnailUrl="https://saltycrush.com/cdn/shop/collections/shirt_dresses.jpg?crop=center&height=300&v=1774847561&width=300%22%7D"},



    };
        }
    }
}
