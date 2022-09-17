using PayCore.Application.Models;

namespace PayCore.Application.ViewModel.User;

public class UserViewModel : BaseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
