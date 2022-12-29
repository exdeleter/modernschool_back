using ModernSchool.Worker;
using ModernSchool.Worker.Interfaces;
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtension
{
    public static IServiceCollection ServiceExtensionConfigure(this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        
        builder.Services.AddTransient<IStudent, EFStudentRepository>();
        
        return services;
    }
}
