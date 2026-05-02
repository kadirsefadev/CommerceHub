using CommerceHub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Services
{
	public class PasswordHasher : IPasswordHasher
	{
		public string PasswordHash(string password)=>BCrypt.Net.BCrypt.HashPassword(password);
		

		public bool VerifyPassword(string password, string hash)=>BCrypt.Net.BCrypt.Verify(password, hash);
		
	}
}
