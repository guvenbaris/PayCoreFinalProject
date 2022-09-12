namespace PayCore.Application.Models
{
    public class ContainerModel : BaseModel
    {
        public virtual string? ContainerName { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual decimal Longitude { get; set; }
        public virtual long VehicleId { get; set; }
    }
}
