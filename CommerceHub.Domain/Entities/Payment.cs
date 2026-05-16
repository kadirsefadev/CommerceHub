
using CommerceHub.Domain.Enums;

namespace CommerceHub.Domain.Entities;

public class Payment: BaseEntity
{

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public string? TransactionId { get; set; }
    public string? FailureReason { get; set; }
    public DateTime? PaidAt { get; set; }

    public string? CardLastFour { get; set; }

}
