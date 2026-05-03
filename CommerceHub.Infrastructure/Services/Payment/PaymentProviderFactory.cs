using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Services.Payment
{
	public static class PaymentProviderFactory
	{

		public static IPaymentProvider Create(string providerName="fake")
		{
			return providerName.ToLower() switch
			{
				"fake" => new FakePaymentProvider(),
				_ => throw new InvalidOperationException($"Bilinmeyen ödeme sağlayıcısı :{providerName} ")
			};


		}

	}
}
