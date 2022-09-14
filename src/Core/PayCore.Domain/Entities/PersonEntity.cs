namespace PayCore.Domain.Entities;

public  class PersonEntity : BaseEntity
{
    public virtual string? FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string IdentityNumber { get; set; }
    public virtual string Email { get; set; }
    public virtual string PhoneNumber { get; set; }
    public virtual string LicencePlateNumber { get; set; }
    public virtual string UserName { get; set; }
    public virtual string Password { get; set; }
    public virtual string Role { get; set; }
    public virtual DateTime? LastActivity { get; set; }
}