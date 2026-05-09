using CommerceHub.Application.DTOs.Payment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Validators
{
	public class PaymentRequestValidator:AbstractValidator<PaymentRequestDto>
	{

		public PaymentRequestValidator()
		{
			RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Geçerli sipariş ID gerekli");
			RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Kart Numarası Boş Olamaz").Length(16).WithMessage("Kart numarası 16 haneli olmalıdır");

			RuleFor(x => x.CardHolderNumner).NotEmpty().WithMessage("Kart üzerindeki isim boş olamaz");
			RuleFor(x => x.Cvv).NotEmpty().WithMessage("cvv boş olamaz").Length(3).WithMessage("Cvv numarası 3 haneli olmalıdır");

			RuleFor(x => x.ExpiryMonth).NotEmpty().WithMessage("Son kullanma ayı boş olamaz");
			RuleFor(x => x.ExpiryYear).NotEmpty().WithMessage("Son kullanma yılı boş olamaz");
		}
	}
}
