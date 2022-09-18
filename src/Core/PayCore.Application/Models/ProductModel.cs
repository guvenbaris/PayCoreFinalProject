namespace PayCore.Application.Models;

public class ProductModel : BaseModel
{
    public string ProductName { get; set; }
    public string Description { get; set; }
    public bool IsOfferable { get; set; } = false;
    public bool IsSold { get; set; } = false;
    public long? CategoryId { get; set; }
    public long? BrandId { get; set; }
    public long? ColorId { get; set; }
    public long? UserId { get; set; }
    public  decimal Price { get; set; }
}
