using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Mapping;

public class UserMapping : ClassMapping<UserEntity>
{
    public UserMapping()
    {
        Table("User");
        Id(x => x.Id,
            x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });
        Property(x => x.FirstName,
            x =>
            {
                x.Column("first_name");
                x.Length(50);
                x.Type(NHibernateUtil.String);
            });
        Property(x => x.LastName,
            x =>
            {
                x.Column("last_name");
                x.Length(50);
                x.Type(NHibernateUtil.String);
            });
        Property(x => x.IdentityNumber,
            x =>
            {
                x.Column("identity_number");
                x.Length(11);
                x.Type(NHibernateUtil.String);
            });
        Property(x => x.PhoneNumber,
            x =>
            {
                x.Column("phone_number");
                x.Length(12);
                x.Type(NHibernateUtil.String);
            });
        Property(x => x.LicencePlateNumber,
            x =>
            {
                x.Column("licence_plate_number");
                x.Length(8);
                x.Type(NHibernateUtil.String);
            });
        Property(x => x.IsDeleted,
            x =>
            {
                x.Column("is_deleted");
                x.Type(NHibernateUtil.Boolean);
            });
    }
}
