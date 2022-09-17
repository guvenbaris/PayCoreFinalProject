
namespace PayCore.Application.Models;

public class OfferModel : BaseModel
{
    public bool IsApproved { get; set; }
    public decimal OfferedPrice { get; set; }
    public long? ProductId { get; set; }
    public int PercentRate { get; set; } = 40;
    public long? UserId { get; set; }
    public UserModel? User { get; set; }
    public ProductModel? Product { get; set; }
}
