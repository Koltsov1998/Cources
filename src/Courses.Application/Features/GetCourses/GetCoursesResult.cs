using System.Collections.Immutable;
using Courses.Core;

namespace Courses.Application.Features.GetCourses;

public record GetCoursesResult(ImmutableArray<Course> Items, int TotalCount);
