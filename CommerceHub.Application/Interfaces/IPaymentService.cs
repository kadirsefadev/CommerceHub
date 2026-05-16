using CommerceHub.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResultDto> ProcessPaymentAsync(int userId, PaymentRequestDto dto);
    }
}
