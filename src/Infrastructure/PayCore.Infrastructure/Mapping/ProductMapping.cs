using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore.Domain.Entities;

namespace PayCore.Infrastructure.Mapping
{
    public class ProductMapping : ClassMapping<ProductEntity>
    {
        public ProductMapping()
        {
            Table("product");
            Lazy(false);
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("Id");
                x.Generator(Generators.Increment);
            });
            Property(b => b.ProductName, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("product_name");
                x.NotNullable(true);
                x.Length(100);
            });
            Property(b => b.Price, x =>
            {
                x.Type(NHibernateUtil.Decimal);
                x.Column("price");
                x.NotNullable(true);
            });
            ManyToOne(c => c.User, p =>
            {
                p.Column("user_id");
                p.Fetch(FetchKind.Join);
                p.NotNullable(true);
                Lazy(false);
            });
            ManyToOne(c => c.Category, p =>
            {
                p.Column("category_id");
                p.Fetch(FetchKind.Join);
                p.NotNullable(true);
                Lazy(false);
            });
            ManyToOne(c => c.Brand, p =>
            {
                p.Column("brand_id");
                p.Fetch(FetchKind.Join);
                p.NotNullable(true);
                Lazy(false);
            });
            ManyToOne(c => c.Color, p =>
            {
                p.Column("color_id");
                p.Fetch(FetchKind.Join);
                p.NotNullable(true);
                Lazy(false);
            });
            Property(b => b.Description, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("description");
                x.NotNullable(true);
                x.Length(500);
            });
            Property(b => b.IsOfferable, x =>
            {
                x.Type(NHibernateUtil.Boolean);
                x.Column("is_offerable");
                x.NotNullable(true);
            });
            Property(b => b.IsSold, x =>
            {
                x.Type(NHibernateUtil.Boolean);
                x.Column("is_sold");
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
