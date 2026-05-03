using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Services.Payment
{
	public interface IPaymentProvider
	{
		Task<PaymentProviderResult> ChargeAsync(PaymentProviderRequest paymentProviderRequest);
	}
	//bir metotta birden fazla değer dondurmek istedigimizde record kullanabiliriz. record c#9 ile birlikte gelen bir özelliktir. veri taşıma dto benzeri servis ici gecici objeler icin record kullanılır
	public record PaymentProviderRequest(

		string CardNumber,
		string CardHolderName,
		string ExpiryMonth,
		string ExpiryYear,
		string Cvv,
		decimal Amount
	);
	public record PaymentProviderResult(

		bool IsSuccess,
		string? TransactionId,
		string? FaileruReason

		);

}
