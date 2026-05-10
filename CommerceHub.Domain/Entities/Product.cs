
namespace CommerceHub.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal  Price { get; set; }
    public int  StockQuantity { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

	//FK 
	public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<OrderItem> OrderItems { get; set; }= new List<OrderItem>();


}
