using Courses.Application.Repositories;
using MediatR;

namespace Courses.Application.Features.GetCourse;

public class GetCourseHandler : IRequestHandler<GetCourseQuery, GetCourseResult?>
{
  private readonly ICourseRepository _courseRepository;

  public GetCourseHandler(ICourseRepository courseRepository)
  {
    _courseRepository = courseRepository;
  }

  public async Task<GetCourseResult?> Handle(GetCourseQuery request, CancellationToken cancellationToken)
  {
    var course = await _courseRepository.Get(request.CurrencyName, request.DateUtc, cancellationToken);

    return course != null ? new GetCourseResult(course.Date, course.Value) : null;
  }
}
