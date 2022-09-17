namespace PayCore.Domain.Entities;

public class UserEntity : BaseEntity
{
    public virtual string FirstName  { get; set; }
    public virtual string LastName  { get; set; }
    public virtual string Email { get; set; }
    public virtual string Password { get; set; }
    public virtual string Role { get; set; }
    public virtual DateTime LastActivity { get; set; } = DateTime.UtcNow;
    public virtual bool LockoutEnabled { get; set; } = false;
    public virtual int? AccessFailedCount { get; set; }
}
