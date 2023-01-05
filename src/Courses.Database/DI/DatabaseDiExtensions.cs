using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Courses.Database.DI;

public static class DatabaseDiExtensions
{
  public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
  {
    return services.AddDbContext<CoursesDbContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("CoursesDbContext"));
    });
  }
}
