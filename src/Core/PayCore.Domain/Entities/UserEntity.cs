namespace PayCore.Domain.Entities;

public class UserEntity : BaseEntity
{
    public virtual string Email { get; set; }
    public virtual string Password { get; set; }
    public virtual string Role { get; set; }
    public virtual DateTime LastActivity { get; set; } = DateTime.Now;
    public virtual bool LockoutEnabled { get; set; }
    public virtual int AccessFailedCount { get; set; }
}
