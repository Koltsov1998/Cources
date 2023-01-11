using Courses.Application.Repositories;
using Courses.Core.Providers;
using MediatR;

namespace Courses.Application.Features.RefreshCourses;

public class RefreshCoursesHandler : IRequestHandler<RefreshCoursesQuery>
{
  private readonly ICoursesProvider _coursesProvider;
  private readonly ICourseRepository _courseRepository;

  public RefreshCoursesHandler(
    ICoursesProvider coursesProvider,
    ICourseRepository courseRepository)
  {
    _coursesProvider = coursesProvider;
    _courseRepository = courseRepository;
  }

  public async Task<Unit> Handle(RefreshCoursesQuery request, CancellationToken cancellationToken)
  {
    var courses = await _coursesProvider.GetCourses(request.Year, cancellationToken);

    await _courseRepository.Add(courses, cancellationToken);

    return Unit.Value;
  }
}
