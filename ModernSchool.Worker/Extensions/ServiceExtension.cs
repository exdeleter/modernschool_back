using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModernSchool.Worker;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtension
{
    public static IServiceCollection ServiceExtensionConfigure(this IServiceCollection services)
    {
        
        services.AddTransient<IStudent, EFStudentRepository>();

        services.AddTransient<DbContext, SchoolDBContext>();
        return services;
    }
    
    public static IServiceCollection AddAuthorization(this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder
                            .Configuration
                            .GetSection("AppSettings:Token")
                            .Value
                        )
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }
}
