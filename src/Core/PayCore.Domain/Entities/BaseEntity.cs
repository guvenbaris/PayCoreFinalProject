namespace PayCore.Domain.Entities;

public class BaseEntity
{
    public virtual long Id { get; set; }
    public virtual bool IsDeleted { get; set; } = false;
}
