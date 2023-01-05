using Courses.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.Application.Features.GetCourse;

public class GetCourseHandler : IRequestHandler<GetCourseQuery, GetCourseResult?>
{
  private readonly CoursesDbContext _coursesDbContext;

  public GetCourseHandler(CoursesDbContext coursesDbContext)
  {
    _coursesDbContext = coursesDbContext;
  }

  public async Task<GetCourseResult?> Handle(GetCourseQuery request, CancellationToken cancellationToken)
  {
    var courseDbo = await _coursesDbContext.Courses
      .OrderByDescending(course => course.Date)
      .Where(course => course.Date <= request.DateUtc)
      .FirstOrDefaultAsync(cancellationToken);

    return courseDbo != null ? new GetCourseResult(courseDbo.Date, courseDbo.Value) : null;
  }
}
