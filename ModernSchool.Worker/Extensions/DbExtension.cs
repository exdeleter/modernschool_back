using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Contexts;

namespace Microsoft.Extensions.DependencyInjection;

public static class DbExtension
{
    public static IServiceCollection AddDbContextConfig(this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.AddDbContext<SchoolDBContext>(
            options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.LogTo(message => Debug.WriteLine(message));
            });

        return services;
    }
}
