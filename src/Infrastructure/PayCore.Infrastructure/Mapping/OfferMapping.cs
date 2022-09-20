using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Mapping
{
    public class OfferMapping : ClassMapping<OfferEntity>
    {
        public OfferMapping()
        {
            Table("offer");
            Lazy(false);
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("Id");
                x.Generator(Generators.Increment);
            });
            ManyToOne(c => c.Product, p =>
            {
                p.Column("product_id");
                p.NotNullable(true);
                p.Fetch(FetchKind.Join);
                Lazy(true);
            });
            ManyToOne(c => c.User, p =>
            {
                p.Column("user_id");
                p.Fetch(FetchKind.Join);
                p.NotNullable(true);
                Lazy(false);
            });
            Property(b => b.OfferedPrice, x =>
            {
                x.Type(NHibernateUtil.Decimal);
                x.Column("offered_price");
                x.NotNullable(true);
            });
            Property(b => b.IsApproved, x =>
            {
                x.Type(NHibernateUtil.Boolean);

                x.Column("is_approved");
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
}
