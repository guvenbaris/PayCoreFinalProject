using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayCore.Application.Interfaces.Sessions;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Domain.Entities;
using PayCore.Infrastructure.Sessions;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using PayCore.Infrastructure.UnitOfWork;
using PayCore.Application.Utilities.Appsettings;

namespace PayCore.Infrastructure.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configurationManager)
        {
            IConfigurationSection appSettingsSection = configurationManager.GetSection("PayCoreSettings");
            services.Configure<PayCoreAppSettings>(appSettingsSection);

            var mapper = new ModelMapper();
            string connectionString = configurationManager.GetConnectionString("PostgreSql");
            mapper.AddMappings(typeof(DependencyContainer).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<PostgreSQLDialect>();
                c.ConnectionString = connectionString;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.SchemaAction = SchemaAutoAction.Update;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });
            configuration.AddMapping(domainMapping);

            var sessionFactory = configuration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            services.AddScoped<IMapperSession<BaseEntity>, MapperSession<BaseEntity>>();
            services.AddScoped(typeof(IUnitOfWork<,>), typeof(UnitOfWork<,>));

            services.AddScoped<IUserSession, UserSession>();
            services.AddScoped<IProductSession, ProductSession>();
            services.AddScoped<ICategorySession, CategorySession>();
            services.AddScoped<IOfferSession, OfferSession>();
            services.AddScoped<IBrandSesion, BrandSession>();
            services.AddScoped<IColorSession, ColorSession>();

            return services;
        }
    }
}
