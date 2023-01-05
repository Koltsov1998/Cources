using System.Collections.Immutable;
using Courses.Application.Features.GetCourse;
using Courses.Controllers.Models;
using Courses.Core;

namespace Courses.Controllers.Mappings;

public static class CoursesMapping
{
  public static GetCoursesResponse MapToResponse(this ImmutableArray<Course> model)
  {
    return new GetCoursesResponse(
      model
        .Select(course => new GetCoursesResponse.CourseDto(
          course.Date,
          course.Value))
        .ToImmutableArray());
  }

  public static GetCourseResponse MapToResponse(this GetCourseResult? model)
  {
    return new GetCourseResponse(
      model?.ExactDate,
      model?.Value);
  }
}
