using CommerceHub.Application.DTOs.Auth;
using CommerceHub.Application.Exceptions;
using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Domain.Enums;
using CommerceHub.Persistence.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPasswordHasher _passwordHasher;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IEmailService _emailService;

		public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService, IEmailService emailService)
		{
			_unitOfWork = unitOfWork;
			_passwordHasher = passwordHasher;
			_jwtTokenService = jwtTokenService;
			_emailService = emailService;
		}



		public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
		{
			var user = await _unitOfWork.Users.FirstOrDefaultAsync(x => x.Email == loginRequestDto.Email)
			   ?? throw new AppException("Email veya şifre hatalıdır");
			if (!user.IsActive)
				throw new AppException("Hesabınız aktif değildir");
			if (!_passwordHasher.VerifyPassword(loginRequestDto.Password, user.PasswordHash))
				throw new AppException("Email veya şifre hatalıdır");


			return BuildAuthResponse(user);

		}

		public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
		{
			var existing = await _unitOfWork.Users.FirstOrDefaultAsync(x=>x.Email==registerRequestDto.Email);
			if (existing != null)
				throw new AppException("Bu email adresi zaten kayıtlıdır");
			var user = new User
			{
				FirstName = registerRequestDto.FirstName,
				LastName = registerRequestDto.LastName,
				Email = registerRequestDto.Email,
				PasswordHash = _passwordHasher.PasswordHash(registerRequestDto.Password),
				Role = UserRoles.Customer

			};
			await _unitOfWork.Users.AddAsync(user);
			await _unitOfWork.SaveChangesAsync();

			await _emailService.SendAsync(user.Email, "Commerce Hub'a Hosgeldinz", $"Merhaba {user.FirstName},hesabınız olusturuldu");

			return  BuildAuthResponse(user);

		}

		private AuthResponseDto BuildAuthResponse(User user) => new()
		{
			Token = _jwtTokenService.GenerateToken(user),
			Email = user.Email,
			FullName = $"{user.FirstName}{user.FirstName}",
			Role = user.Role,
			ExpiresAt = _jwtTokenService.GetExpriy()
		};
	}
}
