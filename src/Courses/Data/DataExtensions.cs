using Courses.Application.Features.RefreshCourses;
using MediatR;

namespace Courses.Data;

public static class DataExtensions
{
  public static async Task SeedData(this IServiceProvider serviceProvider)
  {
    for (var i = 2015; i <= 2021; i++)
    {
      using var scope = serviceProvider.CreateScope();
      var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

      Console.WriteLine($"Downloading courses for year {i}...");
      await mediator.Send(new RefreshCoursesQuery(i), CancellationToken.None);
    }
  }
}
