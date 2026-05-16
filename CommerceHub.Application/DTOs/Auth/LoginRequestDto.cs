using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.DTOs.Auth
{
	public class LoginRequestDto
	{

		public string  Email { get; set; }=string.Empty;
		public string  Password { get; set; } = string.Empty;
	}
	public class RegisterRequestDto
	{
		public string  FirstName { get; set; } = string.Empty;
		public string  LastName { get; set; } = string.Empty;
		public string  Email { get; set; } = string.Empty;
		public string  Password { get; set; } = string.Empty;

	}
	public class AuthResponseDto
	{
		public string  Token { get; set; } = string.Empty;
		public string  Email { get; set; } = string.Empty;
		public string FullName { get; set; } = string.Empty;
		public string  Role { get; set; } = string.Empty;
		public DateTime ExpiresAt { get; set; }
	}
}
