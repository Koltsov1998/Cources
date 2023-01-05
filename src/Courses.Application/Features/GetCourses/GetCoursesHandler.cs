using System.Collections.Immutable;
using Courses.Core;
using Courses.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.Application.Features.GetCourses;

public class GetCoursesHandler : IRequestHandler<GetCoursesQuery, ImmutableArray<Course>>
{
  private readonly CoursesDbContext _dbContext;

  public GetCoursesHandler(CoursesDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<ImmutableArray<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
  {
    var courses = _dbContext.Courses.AsQueryable();

    if (request.DateFromUtc != null) courses = courses.Where(course => course.Date >= request.DateFromUtc);

    if (request.DateToUtc != null) courses = courses.Where(course => course.Date <= request.DateToUtc);

    courses = courses.Where(course => course.CurrencyName == request.CurrencyName);

    var coursesDbo = await courses
      .OrderBy(course => course.Date)
      .Skip(request.PageNumber * request.PageSize)
      .Take(request.PageSize)
      .ToArrayAsync(cancellationToken);

    return coursesDbo
      .Select(courseDbo => new Course(
        courseDbo.Date,
        courseDbo.CurrencyName,
        courseDbo.Value))
      .ToImmutableArray();
  }
}
