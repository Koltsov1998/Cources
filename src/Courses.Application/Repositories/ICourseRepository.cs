using System.Collections.Immutable;
using Courses.Core;

namespace Courses.Application.Repositories;

public interface ICourseRepository
{
  public Task Add(ImmutableArray<Course> courses, CancellationToken cancellationToken);

  public Task<ImmutableArray<string>> GetAllCurrencyNames(CancellationToken cancellationToken);

  public Task<(ImmutableArray<Course> items, int totalCount)> Get(
    string currencyName,
    int? skip,
    int? take,
    DateTime? dateFromUtc,
    DateTime? dateToUtc,
    CancellationToken cancellationToken);

  public Task<Course?> Get(
    string currencyName,
    DateTime dateUtc,
    CancellationToken cancellationToken);
}
