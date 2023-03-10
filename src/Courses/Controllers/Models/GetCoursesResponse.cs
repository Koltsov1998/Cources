using System.Collections.Immutable;

namespace Courses.Controllers.Models;

public record GetCoursesResponse(ImmutableArray<GetCoursesResponse.CourseDto> Courses, int TotalCount)
{
  public record CourseDto(DateTime Date, decimal Value);
}
