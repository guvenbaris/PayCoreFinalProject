namespace PayCore.Domain.Entities;

public abstract class BaseEntity
{
    public virtual long Id { get; set; }
    public virtual bool IsDeleted { get; set; } = false;
}
