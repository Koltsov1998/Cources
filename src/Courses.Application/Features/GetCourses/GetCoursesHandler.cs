using Courses.Application.Repositories;
using MediatR;

namespace Courses.Application.Features.GetCourses;

public class GetCoursesHandler : IRequestHandler<GetCoursesQuery, GetCoursesResult>
{
  private readonly ICourseRepository _courseRepository;

  public GetCoursesHandler(ICourseRepository courseRepository)
  {
    _courseRepository = courseRepository;
  }

  public async Task<GetCoursesResult> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
  {
    var courses = await _courseRepository.Get(
      request.CurrencyName,
      request.PageNumber * request.PageSize,
      request.PageSize,
      request.DateFromUtc,
      request.DateToUtc,
      cancellationToken);

    return new GetCoursesResult(courses.items, courses.totalCount);
  }
}
