using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Mapping
{
    public class ColorMapping : ClassMapping<ColorEntity>
    {
        public ColorMapping()
        {
            Table("color"); 

            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("Id");
                x.Generator(Generators.Increment);
            });

            Property(b => b.ColorName, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("color_name");
                x.NotNullable(true);
                x.Length(100);
            });
            Property(b => b.IsDeleted, x =>
            {
                x.Type(NHibernateUtil.Boolean);
                x.Column("is_deleted");
                x.NotNullable(true);
            });
        }
    }
}