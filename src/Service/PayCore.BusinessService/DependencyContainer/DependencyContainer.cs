using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Interfaces.HelperServices;
using PayCore.Application.Interfaces.Jwt;
using PayCore.Application.Interfaces.Services;
using PayCore.BusinessService.Cache;
using PayCore.BusinessService.HelperServices;
using PayCore.BusinessService.Services;
using StackExchange.Redis;
using System.Text;

namespace PayCore.BusinessService.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["PayCoreSettings:JwtSettings:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["PayCoreSettings:JwtSettings:Key"])),
                    ValidAudience = configuration["PayCoreSettings:JwtSettings:Audience"],
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });

            ConfigurationOptions configurationOptions = new ConfigurationOptions()
            {
                EndPoints = {$"{configuration.GetSection("PayCoreSettings:RedisSettings").GetValue<string>("Host") }:{configuration.GetSection("PayCoreSettings:RedisSettings").GetValue<string>("Port")}"
            },
                AllowAdmin = true,
                ConnectTimeout = 60 * 1000,
            };

            services.AddStackExchangeRedisCache(options => { options.Configuration = configurationOptions.ToString(); });

            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IAccountDetailService, AccountDetailService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IBrandService, BrandService>();


            return services;
        }
    }
}
