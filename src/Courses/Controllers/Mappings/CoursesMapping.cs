using System.Collections.Immutable;
using Courses.Application.Features.GetCourse;
using Courses.Application.Features.GetCourses;
using Courses.Controllers.Models;
using Courses.Core;

namespace Courses.Controllers.Mappings;

public static class CoursesMapping
{
  public static GetCoursesResponse MapToResponse(this GetCoursesResult model)
  {
    return new GetCoursesResponse(
      model.Items
        .Select(course => new GetCoursesResponse.CourseDto(
          course.Date,
          course.Value))
        .ToImmutableArray(),
      model.TotalCount);
  }

  public static GetCourseResponse MapToResponse(this GetCourseResult? model)
  {
    return new GetCourseResponse(
      model?.ExactDate,
      model?.Value);
  }
}
