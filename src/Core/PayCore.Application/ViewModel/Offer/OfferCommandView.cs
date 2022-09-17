
using PayCore.Application.Models;

namespace PayCore.Application.ViewModel.Offer;

public class OfferCommandView : BaseModel
{
    public int PercentRate { get; set; }
    public decimal OfferedPrice { get; set; }
    public long ProductId { get; set; }
    public long UserId { get; set; }
}
