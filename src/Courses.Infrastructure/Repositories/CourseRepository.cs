using System.Collections.Immutable;
using Courses.Application.Repositories;
using Courses.Core;
using Courses.Database;
using Courses.Database.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
  private readonly CoursesDbContext _dbContext;

  public CourseRepository(CoursesDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task Add(ImmutableArray<Course> courses, CancellationToken cancellationToken)
  {
    var courseDbos = courses.Select(course =>
        new CourseDbo(
          DateTime.SpecifyKind(course.Date, DateTimeKind.Utc),
          course.CurrencyName,
          course.Value))
      .ToList();

    await _dbContext.BulkInsertOrUpdateAsync(courseDbos.ToList(), cancellationToken: cancellationToken);
  }

  public async Task<ImmutableArray<string>> GetAllCurrencyNames(CancellationToken cancellationToken)
  {
    return (await _dbContext.Courses
        .Select(course => course.CurrencyName)
        .Distinct()
        .ToArrayAsync(cancellationToken))
      .ToImmutableArray();
  }

  public async Task<(ImmutableArray<Course> items, int totalCount)> Get(
    string currencyName,
    int? skip,
    int? take,
    DateTime? dateFromUtc,
    DateTime? dateToUtc,
    CancellationToken cancellationToken)
  {
    var courses = _dbContext.Courses
      .OrderBy(course => course.Date)
      .AsQueryable();

    if (dateFromUtc != null) courses = courses.Where(course => course.Date >= dateFromUtc);

    if (dateToUtc != null) courses = courses.Where(course => course.Date <= dateToUtc);

    courses = courses.Where(course => course.CurrencyName == currencyName);

    var totalCount = await courses.CountAsync(cancellationToken);

    if (skip != null)
    {
      courses = courses
        .Skip(skip.Value);
    }
    if (take != null)
    {
      courses = courses
        .Take(take.Value);
    }

    var coursesDbo = await courses
      .ToArrayAsync(cancellationToken);

    return (coursesDbo
      .Select(courseDbo => new Course(
        courseDbo.Date,
        courseDbo.CurrencyName,
        courseDbo.Value))
      .ToImmutableArray(), totalCount);
  }

  public async Task<Course?> Get(string currencyName, DateTime dateUtc, CancellationToken cancellationToken)
  {
    var courseDbo = await _dbContext.Courses
      .OrderByDescending(course => course.Date)
      .Where(course => course.Date <= dateUtc && course.CurrencyName == currencyName)
      .FirstOrDefaultAsync(cancellationToken);

    return courseDbo != null ? new Course(
      courseDbo.Date,
      courseDbo.CurrencyName,
      courseDbo.Value) : null;
  }
}
