using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommerceHub.Application.Interfaces.Payment;

namespace CommerceHub.Infrastructure.Services.Payment
{
	//iyzico,payu,sipay,kerzzpos
	public class FakePaymentProvider : IPaymentProvider
	{
		private const string BlokedCard = "1111111111111111";
		public Task<PaymentProviderResult> ChargeAsync(PaymentProviderRequest paymentProviderRequest)
		{
			//=>  Task.FromResult(Validate(request))
			throw new NotImplementedException();
		}
		private static PaymentProviderResult Validate(PaymentProviderRequest paymentProviderRequest)
		{
			if (paymentProviderRequest.Amount <= 0)
				return Fail("Odeme tutarı 0 dan kucuk olamaz");
			if (paymentProviderRequest.CardNumber.Length < 16)
				return Fail("Kart numarası kucuk olamaz 16 haneden lütfen 16 hane giriniz");
			if (paymentProviderRequest.Cvv.Length < 3)
				return Fail("Cvv numarası 3 haneli olmalıdır");
			if (paymentProviderRequest.CardHolderName.Length <= 2)
				return Fail("Name alanı en az 3 haneli olmalıdır");
			if (paymentProviderRequest.CardNumber == BlokedCard)
				return Fail("Bu kart ile işlem yapılamaz bloked card");
			var transactionId = $"TXN - {Guid.NewGuid():N}".ToUpper()[..20];
			return new PaymentProviderResult(true, transactionId, null);

		}
		private static PaymentProviderResult Fail(string reason)
			=> new PaymentProviderResult(false,null,reason);
	}
}
