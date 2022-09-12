using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Mapping;
public class ContainerMapping : ClassMapping<Container>
{
    public ContainerMapping()
    {
        Table("Container");
        Id(x => x.Id,
            x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });
        Property(x => x.ContainerName,
            x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
            });
        Property(x => x.Latitude,
            x =>
            {
                x.Type(NHibernateUtil.Currency);
                x.Precision(10);
                x.Scale(6);
            });
        Property(x => x.Longitude,
            x =>
            {
                x.Type(NHibernateUtil.Currency);
                x.Precision(10);
                x.Scale(6);
            });
        Property(x => x.VehicleId,
            x =>
            {
                x.Type(NHibernateUtil.Int64);
            });
    }
}
