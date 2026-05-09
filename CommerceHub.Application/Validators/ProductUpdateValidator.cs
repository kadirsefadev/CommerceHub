using CommerceHub.Application.DTOs.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Validators
{
	public class ProductUpdateValidator:AbstractValidator<ProductUpdateDto>
	{
		public ProductUpdateValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün Adı Boş olamaz").MaximumLength(200).WithMessage("Urun Adi 200 karakterden fazla olamaz");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Fiyat 0 dan büyük olmalıdır.");
			RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stok Negatif Olamaz");
			RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Kategori Seçilmelidir.");
		}
	}
}
