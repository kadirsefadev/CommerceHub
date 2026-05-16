using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Interfaces.Payment
{
	public interface IPaymentProviderFactory
	{
		IPaymentProvider Create(string providerName = "fake");
	}
}
