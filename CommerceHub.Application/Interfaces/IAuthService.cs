using CommerceHub.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Interfaces
{
	public interface IAuthService
	{

		Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
		Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
	}
}
