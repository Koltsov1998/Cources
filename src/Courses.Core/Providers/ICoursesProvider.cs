using System.Collections.Immutable;

namespace Courses.Core.Providers;

public interface ICoursesProvider
{
  Task<ImmutableArray<Course>> GetCourses(int year, CancellationToken cancellationToken);
}
