

namespace CommerceHub.Domain.Enums;

public enum PaymentStatus
{
    Pending = 1,    // Odeme Bekleniyor
    Success = 2,    // Odeme Basarili
    Failed = 3,     // Odeme Basarisiz
    Refunded = 4    // Iade Edildi
}

