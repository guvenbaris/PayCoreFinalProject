using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore.Domain.Entities;


namespace PayCore.Infrastructure.Mapping
{
    public class PersonMapping : ClassMapping<PersonEntity>
    {
        public PersonMapping()
        {
            Table("Person");
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
                    x.NotNullable(true);
                });
            Property(x => x.LastName,
                x =>
                {
                    x.Column("last_name");
                    x.Length(50);
                    x.Type(NHibernateUtil.String);
                    x.NotNullable(true);
                });
            Property(x => x.IdentityNumber,
                x =>
                {
                    x.Column("identity_number");
                    x.Length(11);
                    x.Type(NHibernateUtil.String);
                    x.NotNullable(true);
                });
            Property(x => x.PhoneNumber,
                x =>
                {
                    x.Column("phone_number");
                    x.Length(12);
                    x.Type(NHibernateUtil.String);
                    x.NotNullable(true);
                });
            Property(b => b.Email, 
                x =>
                {
                    x.Length(150);
                    x.Type(NHibernateUtil.String);
                });
            Property(x => x.LicencePlateNumber,
                x =>
                {
                    x.Column("licence_plate_number");
                    x.Length(8);
                    x.Type(NHibernateUtil.String);
                });
            Property(b => b.UserName,
                x =>
                {
                    x.Length(150);
                    x.Type(NHibernateUtil.String);
                    x.NotNullable(true);
                });
            Property(b => b.Password, 
                x =>
                {
                    x.Length(150);
                    x.Type(NHibernateUtil.String);
                    x.NotNullable(true);
                });

            Property(b => b.Role, 
                x =>
                {
                    x.Length(50);
                    x.Type(NHibernateUtil.String);
                    x.NotNullable(true);
                });

            Property(b => b.LastActivity, 
                x =>
                {
                    x.Type(NHibernateUtil.DateTime);
                    x.NotNullable(true);
                });
            Property(x => x.IsDeleted,
                x =>
                {
                    x.Column("is_deleted");
                    x.Type(NHibernateUtil.Boolean);
                    x.NotNullable(true);
                });
        }
    }
}
