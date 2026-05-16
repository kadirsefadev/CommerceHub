

namespace CommerceHub.Domain.Enums;

public enum OrderStatus
{
    Pending=1, //Odeme Bekleniyor
    Paid=2, //Odeme Alindi
    Confirmed= 3, //Siparis Onaylandi
    Shipped = 4, //Kargoya Verildi
    Delivered = 5, //Teslim Edildi
    Cancelled = 6, //Iptal Edildi
    PaymentFailed = 7 //Odeme Basarisiz

}
