using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Common
{
	public class ApiResponse<T>
	{
		public bool Success { get; set; }
		public string  Message { get; set; }=string.Empty;
		public T? Data { get; set; }
		public List<string> Errors { get; set; }=new List<string>();

		public static ApiResponse<T> SuccessResult(T data ,string message="İşlem Başarılıdır.")
			=> new() { Success = true, Message = message ,Data=data};

		public static ApiResponse<T> FailResult(string message, List<string>? errors = null)
			=> new() { Success = false, Message = message, Errors = errors ?? new() };


	}
	public class ApiResponse : ApiResponse<object>
	{
		public static ApiResponse SuccessResult(string message="İşlem Başarıldır")
			=>new() { Success = true,Message = message };
		public new static ApiResponse FailResult(string message,List<string>? errors = null)
			=> new() { Success=false,Message = message,Errors = errors ?? new() };

	}
}
