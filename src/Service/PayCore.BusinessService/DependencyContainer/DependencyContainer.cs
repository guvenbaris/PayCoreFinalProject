using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Interfaces.Services;
using PayCore.BusinessService.Cache;
using PayCore.BusinessService.Services;
using System.Text;

namespace PayCore.BusinessService.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<ICacheService, CacheService>();

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

            return services;
        }
    }
}
