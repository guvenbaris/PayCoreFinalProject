namespace PayCore.Application.Models;

public class UserModel : BaseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;
    public bool LockoutEnabled { get; set; } = false;
    public int? AccessFailedCount { get; set; }
}
