using System.Collections.Immutable;
using Courses.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.Application.Features.GetCountryNames;

public class GetCountryNamesHandler : IRequestHandler<GetCountryNamesQuery, ImmutableArray<string>>
{
  private readonly CoursesDbContext _coursesDbContext;

  public GetCountryNamesHandler(CoursesDbContext coursesDbContext)
  {
    _coursesDbContext = coursesDbContext;
  }

  public async Task<ImmutableArray<string>> Handle(
    GetCountryNamesQuery request,
    CancellationToken cancellationToken)
  {
    return (await _coursesDbContext.Courses
        .Select(course => course.CurrencyName)
        .Distinct()
        .ToArrayAsync(cancellationToken))
      .ToImmutableArray();
  }
}
