using CommerceHub.Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Validators
{
	public class RegisterRequestValidator:AbstractValidator<RegisterRequestDto>
	{
		public RegisterRequestValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim boş geçilemez");
			RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyisim boş geçilemez");
			RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş geçilemez...").EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş gecilemez").MinimumLength(6).WithMessage("Şifre 6 karakterden az olamaz.");
		}
	}
}
