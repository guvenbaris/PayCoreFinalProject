
namespace PayCore.Application.Models
{
    public class BaseModel
    {
        public long? Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
