using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Courses.Database.DI;

public static class MigratorExtensions
{
  public static void Migrate(this IServiceProvider serviceProvider)
  {
    using var scope = serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<CoursesDbContext>();
    dbContext.Database.Migrate();
  }
}
