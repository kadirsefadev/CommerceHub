using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Exceptions
{
	public class AppException:Exception
	{
		public int StatusCode { get; }
		public AppException(string message,int statuscode=400):base(message)
		{
			StatusCode= statuscode;
		}
	}
	public class NotFoundException: AppException
	{
		public NotFoundException(string message) : base(message, 404) { }
	}
	public class ValidationException:AppException
	{
		public List<string> ValidationErrors { get; }
		public ValidationException(List<string> validationErrors):base("Validasyon Hatası", 422)
		{
			ValidationErrors = validationErrors;
		}
	}
	public class UnauthorizedException : AppException
	{
		public UnauthorizedException(string message="Bu işlem için yetkiniz yoktur.") : base(message,403) { }
	}

}
