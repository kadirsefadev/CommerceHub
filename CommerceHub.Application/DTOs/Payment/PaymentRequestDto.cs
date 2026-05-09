using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.DTOs.Payment
{
	public class PaymentRequestDto
	{
		public int OrderId { get; set; }
		public string  CardNumber { get; set; }=string.Empty;
		public string  CardHolderNumner { get; set; } = string.Empty;
		public string ExpiryMonth { get; set; } = string.Empty;
		public string  ExpiryYear { get; set; } = string.Empty;
		public string  Cvv { get; set; } = string.Empty;
	}
	class PaymentResultDto
	{
		public bool  IsSuccess { get; set; }
		public string? TransactionId { get; set; }
		public string?  FailureReason { get; set; }
		public decimal  Amount { get; set; }
	}
}
