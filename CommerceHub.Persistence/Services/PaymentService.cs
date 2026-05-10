using CommerceHub.Application.DTOs.Payment;
using CommerceHub.Application.Exceptions;
using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Domain.Enums;
using CommerceHub.Infrastructure.Services.Payment;
using CommerceHub.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.Services
{
    public class PaymentService :IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;


        public PaymentService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }


        public async Task<PaymentResultDto> ProcessPaymentAsync(int userId, PaymentRequestDto dto)
        {
            var order = await _unitOfWork.Orders.Query()
                .Include(x => x.Payment)
                .FirstOrDefaultAsync(x => x.Id == dto.OrderId && x.UserId == userId)
                ?? throw new NotFoundException($"Sipariş bulunamadı ID: {dto.OrderId}");

            if (order.OrderStatus != OrderStatus.Pending)
                throw new AppException("Sipariş zaten işlenmiş veya iptal edilmiş.");
            var provider = PaymentProviderFactory.Create("fake");

            var providerResult = await provider.ChargeAsync(new PaymentProviderRequest(
                dto.CardNumber,
                dto.CardHolderNumner,
                dto.ExpiryMonth,
                dto.ExpiryYear,
                dto.Cvv,
                order.TotalAmount
                ));
            var payment = new Payment
            {
                OrderId = order.Id,
                Amount = order.TotalAmount,
                Status = providerResult.IsSuccess ? PaymentStatus.Success : PaymentStatus.Failed,
                TransactionId = providerResult.TransactionId,
                FailureReason = providerResult.FaileruReason,
                CardLastFour = dto.CardNumber[^4..],
                PaidAt = providerResult.IsSuccess ? DateTime.UtcNow : (DateTime?)null
            };
            await _unitOfWork.Payments.AddAsync(payment);
            order.OrderStatus= providerResult.IsSuccess ? OrderStatus.Paid : OrderStatus.PaymentFailed;
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();

            if(providerResult.IsSuccess)
            {
                var user= await _unitOfWork.Users.GetByIdAsync(userId);
                if(user!=null)
                {
                    await _emailService.SendAsync(user.Email, "Ödeme Başarılı", $"Siparişiniz için ödemeniz başarıyla alındı. Sipariş ID: {order.Id}");
                }

            }

            return new PaymentResultDto
            {
                IsSuccess = providerResult.IsSuccess,
                TransactionId = providerResult.TransactionId,
                FailureReason = providerResult.FaileruReason,
                Amount = order.TotalAmount

            };


        }
    }
}
