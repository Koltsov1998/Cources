using Courses.Core.Providers;
using Courses.Infrastructure.Clients;
using Courses.Infrastructure.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Courses.Infrastructure.DI;

public static class InfrastructureDiExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .RegisterClients()
            .AddScoped<ICoursesProvider, CoursesProvider>();
    }

    public static IServiceCollection RegisterClients(this IServiceCollection services)
    {
        services
            .AddHttpClient<CoursesHttpClient>()
            .ConfigureHttpClient(
                options => options.BaseAddress = new Uri("https://www.cnb.cz"));

        return services;
    }
}