
namespace PayCore.Application.Models
{
    public abstract class BaseModel
    {
        public long? Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
