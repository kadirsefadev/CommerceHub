using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Services
{
	//Todo: Burada access token üretildi ödev olarak ben buraya refresh token entegre etmeliyim...
	public class JwtTokenService : IJwtTokenService
	{
		private readonly JwtOptions _jwtOptions;

		public JwtTokenService(IOptions<JwtOptions> options)
		{
			_jwtOptions = options.Value;
		}
		public string GenerateToken(User user)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.Role,user.Role),
				new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")
			};
			var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
			var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtOptions.Issuer,
				audience: _jwtOptions.Audience,
				claims: claims,
				signingCredentials: creds,
				expires: GetExpriy());

			   
			 return new JwtSecurityTokenHandler().WriteToken(token);
		
		}

		public DateTime GetExpriy()=>DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes);
		
	}
}
