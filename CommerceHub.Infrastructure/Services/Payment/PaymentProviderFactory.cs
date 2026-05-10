using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommerceHub.Application.Interfaces.Payment;

namespace CommerceHub.Infrastructure.Services.Payment
{
	public class PaymentProviderFactory : IPaymentProviderFactory
	{

		public IPaymentProvider Create(string providerName="fake")
		{
			return providerName.ToLower() switch
			{
				"fake" => new FakePaymentProvider(),
				_ => throw new InvalidOperationException($"Bilinmeyen ödeme sağlayıcısı :{providerName} ")
			};


		}

	}
}
