using System.Collections.Immutable;
using Courses.Application.Repositories;
using Courses.Core;
using MediatR;

namespace Courses.Application.Features.GetCourses;

public class GetCoursesHandler : IRequestHandler<GetCoursesQuery, ImmutableArray<Course>>
{
  private readonly ICourseRepository _courseRepository;

  public GetCoursesHandler(ICourseRepository courseRepository)
  {
    _courseRepository = courseRepository;
  }

  public async Task<ImmutableArray<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
  {
    return (await _courseRepository.Get(
      request.CurrencyName,
      request.PageNumber * request.PageSize,
      request.PageSize,
      request.DateFromUtc,
      request.DateToUtc,
      cancellationToken)).items;
  }
}
