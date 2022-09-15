namespace PayCore.Application.Models;

public class UserModel : BaseModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime LastActivity { get; set; } = DateTime.Now;
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}
