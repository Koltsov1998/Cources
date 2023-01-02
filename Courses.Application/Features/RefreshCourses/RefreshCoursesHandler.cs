using Courses.Core.Providers;
using Courses.Database;
using Courses.Database.Models;
using EFCore.BulkExtensions;
using MediatR;

namespace Courses.Application.Features.RefreshCourses;

public class RefreshCoursesHandler : IRequestHandler<RefreshCoursesQuery>
{
    private readonly ICoursesProvider _coursesProvider;
    private readonly CoursesDbContext _dbContext;

    public RefreshCoursesHandler(
        ICoursesProvider coursesProvider,
        CoursesDbContext dbContext)
    {
        _coursesProvider = coursesProvider;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(RefreshCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _coursesProvider.GetCourses(request.Year, cancellationToken);

        var courseDbos = courses.Select(course => 
            new CourseDbo(
                DateTime.SpecifyKind(course.Date, DateTimeKind.Utc), 
                course.CurrencyName, 
                course.Value))
            .ToList();
        
        await _dbContext.BulkInsertOrUpdateAsync(
            courseDbos, 
            cancellationToken: cancellationToken);
        
        return Unit.Value;
    }
}