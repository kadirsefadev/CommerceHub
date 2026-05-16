using CommerceHub.Application.DTOs.Order;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Validators
{
	public class CreateOrderValidator:AbstractValidator<CreateOrderDto>
	{
		public CreateOrderValidator()
		{
			RuleFor(x => x.ShippingFullName).NotEmpty().WithMessage("Ad Soyad bilgisi giriniz");

			RuleFor(x => x.ShippingPhone).NotEmpty().WithMessage("Telefon numarası boş olamaz");

			RuleFor(x => x.ShippingFullAddress).NotEmpty().WithMessage("Adres boş geçilemez");

			RuleFor(x => x.ShippingCity).NotEmpty().WithMessage("Sehir boş geçilemez");
		}
	}
}
