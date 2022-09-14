using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore.Domain.Entities;
namespace PayCore.Infrastructure.Mapping
{
    public class ApartmentMapping : ClassMapping<ApartmentEntity>
    {
        public ApartmentMapping()
        {
            Table("Apartment");
            Id(x => x.Id,
                x =>
                {
                    x.Type(NHibernateUtil.Int64);
                    x.Column("id");
                    x.UnsavedValue(0);
                    x.Generator(Generators.Increment);
                });
            Property(x => x.WhichBlock,
                x =>
                {
                    x.Column("which_block");
                    x.Length(10);
                    x.Type(NHibernateUtil.String);
                });
            Property(x => x.AparmentStatu,
                x =>
                {
                    x.Column("apartment_statu");
                    x.Type(NHibernateUtil.Boolean);
                });
            Property(x => x.ApartmentType,
                x =>
                {
                    x.Column("apartment_type");
                    x.Length(3);
                    x.Type(NHibernateUtil.String);
                });
            Property(x => x.FloorLocation,
                x =>
                {
                    x.Column("floor_location");
                    x.Type(NHibernateUtil.Int32);

                });
            Property(x => x.ApartmentNumber,
                x =>
                {
                    x.Column("apartment_number");
                    x.Type(NHibernateUtil.Int32);
                });
            Property(x => x.PersonId,
                x =>
                {
                    x.Column("person_id");
                    x.Type(NHibernateUtil.Int64);
                });
            Property(x => x.IsDeleted,
                x =>
                {
                    x.Column("is_deleted");
                    x.Type(NHibernateUtil.Boolean);
                });
        }
    }
}
