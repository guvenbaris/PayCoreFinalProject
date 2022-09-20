using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Mapping;

public class UserMapping : ClassMapping<UserEntity>
{
    public UserMapping()
    {
        Table("user");
        Lazy(false);
        Id(x => x.Id, x =>
        {
            x.Type(NHibernateUtil.Int64);
            x.Column("Id");
            x.Generator(Generators.Increment);
        });
        Property(b => b.FirstName, x =>
        {
            x.Type(NHibernateUtil.String);
            x.Length(50);
            x.Column("first_name");
            x.NotNullable(true);
        });
        Property(b => b.LastName, x =>
        {
            x.Length(50);
            x.Type(NHibernateUtil.String);
            x.Column("last_name");
            x.NotNullable(true);
        });
        Property(b => b.Email, x =>
        {
            x.Length(150);
            x.Type(NHibernateUtil.String);
            x.Column("email");
            x.NotNullable(true);
        });
        Property(b => b.Password, x =>
        {
            x.Type(NHibernateUtil.String);
            x.NotNullable(true);
            x.Column("password");
        });
        Property(b => b.Role, x =>
        {
            x.Type(NHibernateUtil.String);
            x.NotNullable(true);
            x.Column("role");
        });
        Property(b => b.LastActivity, x =>
        {
            x.Type(NHibernateUtil.DateTime);
            x.NotNullable(true);
            x.Column("last_activity");
        });
        Property(b => b.AccessFailedCount, x =>
        {
            x.Type(NHibernateUtil.Int32);
            x.Column("access_failed_count");
        });
        Property(b => b.LockoutEnabled, x =>
        {
            x.Type(NHibernateUtil.Boolean);
            x.Column("lockout_enabled");
            x.NotNullable(true);
        });
        Property(b => b.IsDeleted, x =>
        {
            x.Type(NHibernateUtil.Boolean);
            x.Column("is_deleted");
            x.NotNullable(true);
        });
    }
}
