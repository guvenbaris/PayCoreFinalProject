
namespace PayCore.Application.Models
{
    public abstract class BaseModel
    {
        public bool IsDeleted { get; set; } = false;
    }
}
