using Courses.Core.Providers;
using Courses.Database;
using Courses.Database.Models;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        
        await using var connection = _dbContext.Database.GetDbConnection();

        await connection.ExecuteAsync(@"
            INSERT INTO public.""Courses"" (
                ""Date"",
                ""CurrencyName"",
                ""Value"")
            VALUES (
                @Date,
                @CurrencyName,
                @Value)
            ON CONFLICT (
                ""Date"", 
                ""CurrencyName"") 
            DO UPDATE SET 
                ""Date"" = @Date,
                ""CurrencyName"" = @CurrencyName,
                ""Value"" = @Value", 
            courseDbos);
        
        return Unit.Value;
    }
}