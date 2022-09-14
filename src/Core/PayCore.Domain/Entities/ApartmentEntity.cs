namespace PayCore.Domain.Entities;

public class ApartmentEntity : BaseEntity
{
    public virtual bool AparmentStatu { get; set; }
    public virtual string ApartmentType { get; set; }
    public virtual int ApartmentNumber { get; set; }
    public virtual int FloorLocation { get; set; }
    public virtual long PersonId { get; set; }
    public virtual string WhichBlock { get; set; }
}
