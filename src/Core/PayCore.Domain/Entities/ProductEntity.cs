namespace PayCore.Domain.Entities;

public class ProductEntity : BaseEntity
{
    public virtual string ProductName { get; set; }
    public virtual string Description { get; set; }
    public virtual bool IsOfferable { get; set; } = false;
    public virtual CategoryEntity? Category  { get; set; }
    public virtual bool IsSold { get; set; } = false;
    public virtual UserEntity? User { get; set; }
    public virtual decimal Price { get; set; }
}
