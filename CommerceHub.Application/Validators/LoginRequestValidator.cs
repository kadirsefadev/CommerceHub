using CommerceHub.Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Validators
{
	public class LoginRequestValidator:AbstractValidator<LoginRequestDto>
	{
		public LoginRequestValidator()
		{
			RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş geçilemez...").EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş gecilemez").MinimumLength(6).WithMessage("Şifre 6 karakterden az olamaz.");
		}
	}
}
