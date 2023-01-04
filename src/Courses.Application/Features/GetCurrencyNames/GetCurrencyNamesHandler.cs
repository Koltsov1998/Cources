using System.Collections.Immutable;
using Courses.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.Application.Features.GetCurrencyNames;

public class GetCountryNamesHandler : IRequestHandler<GetCurrencyNamesQuery, ImmutableArray<string>>
{
  private readonly CoursesDbContext _coursesDbContext;

  public GetCountryNamesHandler(CoursesDbContext coursesDbContext)
  {
    _coursesDbContext = coursesDbContext;
  }

  public async Task<ImmutableArray<string>> Handle(
    GetCurrencyNamesQuery request,
    CancellationToken cancellationToken)
  {
    return (await _coursesDbContext.Courses
        .Select(course => course.CurrencyName)
        .Distinct()
        .ToArrayAsync(cancellationToken))
      .ToImmutableArray();
  }
}
