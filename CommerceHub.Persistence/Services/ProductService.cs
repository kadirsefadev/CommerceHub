using CommerceHub.Application.DTOs.Product;
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
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<ProductDetailDto> CreateAsync(ProductCreateDto productDetailDto)
		{
			var categoryExists=await _unitOfWork.Categories.FirstOrDefaultAsync(x=>x.Id== productDetailDto.CategoryId);
			if (categoryExists == null)
				throw new NotFoundException($"Kategori Bulunamadı ID:{productDetailDto.CategoryId}");
			var product = new Product
			{
				Name = productDetailDto.Name,
				CategoryId = productDetailDto.CategoryId,
				Description = productDetailDto.Description,
				Price = productDetailDto.Price,
				StockQuantity = productDetailDto.StockQuantity,
				ThumnailUrl = productDetailDto.ThumbnailUrl
			};
			await _unitOfWork.Products.AddAsync(product);
			await _unitOfWork.SaveChangesAsync();
			return await GetByIdAsync(product.Id);


		}

		public async Task DeleteAsync(int id)
		{

			//2 çeşit silme operasyonu vardır 1: Hard Delete : databaseden tamamen ürünü siler 2: Soft delete bizim belirlemiş oldugumuz IsActive alanı yada IsDelete alanı true ya set edilir bu set etme sonucunda ben ürünü gösterirken sadece IsActive true olanları karsı tarafa gosterirm fiziksel olarak ürün database durur ancak herhangi bir silinme olusmaz sadece durum değişir. 

			var product = await _unitOfWork.Products.GetByIdAsync(id)
				?? throw new NotFoundException($"Urun bulunamadı ID: {id}");
			product.IsDeleted = true;
			product.UpdatedAt = DateTime.UtcNow;
			_unitOfWork.Products.Update(product);
			await _unitOfWork.SaveChangesAsync();



		}

		public async Task<List<ProductListDto>> GetAllAsync(int? categoryId = null)
		{
			var query = _unitOfWork.Products.Query().Include(x => x.Category).Where(x => x.IsActive);
			/*
			 select* from Product p inner join Category c on p.CategoryId=c.CategoryId where IsActive=1 
			 liste var ise getir. categoryId data varmı ? getir.
			business iş yapan katmanda entity,yada domain katman içerisinde yer alan model olmaz. dto cagırılır. 
			 */


			if (categoryId.HasValue)
				query = query.Where(x => x.CategoryId == categoryId.Value);
			return await query.Select(x => new ProductListDto
			{
				Id = x.Id,
				Name = x.Name,
				Price = x.Price,
				StockQuantity = x.StockQuantity,
				ThumbnailUrl = x.ThumnailUrl,
				CategoryName = x.Category.Name,
				IsActive = x.IsActive,

			}).ToListAsync();


		}

		public async Task<ProductDetailDto> GetByIdAsync(int id)
		{
			var product = await _unitOfWork.Products.Query()
				.Include(x => x.Category)
				.Include(x => x.Images)
				.FirstOrDefaultAsync(x => x.Id == id)
				?? throw new NotFoundException($"Urun Bulunamadı ID:{id}");
			return MapToDetailDto(product);
		}

		public async Task<ProductDetailDto> UpdateAsync(int id, ProductUpdateDto productDetailDto)
		{

			var product = await _unitOfWork.Products.GetByIdAsync(id)
				?? throw new NotFoundException($"Urun bulunamadı ID: {id}");
			product.Name = productDetailDto.Name;
			product.Price = productDetailDto.Price;
			product.Description = productDetailDto.Description;
			product.StockQuantity = productDetailDto.StockQuantity;
			product.ThumnailUrl = product.ThumnailUrl;
			product.CategoryId= productDetailDto.CategoryId;
			product.IsActive = productDetailDto.IsActive;
			product.UpdatedAt= DateTime.UtcNow;
			_unitOfWork.Products.Update(product);
			await _unitOfWork.SaveChangesAsync();
			return await GetByIdAsync(id);
		}


		private static ProductDetailDto MapToDetailDto(Product product) => new()
		{
			Id = product.Id,
			Name = product.Name,
			Price = product.Price,
			StockQuantity = product.StockQuantity,
			ThumbnailUrl = product.ThumnailUrl,
			IsActive = product.IsActive,
			CategoryId = product.CategoryId,
			CategoryName = product.Category.Name,
			ImagesUrl = product.Images.Select(x => x.ImageUrl).ToList()
		};


	}
}
