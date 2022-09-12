namespace PayCore.Domain.Entities;

public class Container : BaseEntity
{
    public virtual string? ContainerName { get; set; }
    public virtual decimal Latitude { get; set; }
    public virtual decimal Longitude { get; set; }
    public virtual long VehicleId { get; set; }
}
