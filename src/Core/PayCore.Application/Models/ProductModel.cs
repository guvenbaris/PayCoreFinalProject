using PayCore.Application.ViewModel.User;

namespace PayCore.Application.Models
{
    public class ProductModel : BaseModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public bool IsOfferable { get; set; } = false;
        public bool IsSold { get; set; } = false;
        public CategoryModel? Category { get; set; }
        public long? CategoryId { get; set; }
        public UserViewModel? User { get; set; }
        public long? UserId { get; set; }
        public  decimal Price { get; set; }
    }
}
