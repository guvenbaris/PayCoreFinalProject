
namespace PayCore.Domain.Entities;

public class OfferEntity : BaseEntity
{
    public virtual bool IsApproved { get; set; } = false;
    public virtual decimal OfferedPrice { get; set; }
    public virtual int PercentRate { get; set; } = 40;
    public virtual ProductEntity? Product { get; set; }
    public virtual UserEntity? User { get; set; }
}
