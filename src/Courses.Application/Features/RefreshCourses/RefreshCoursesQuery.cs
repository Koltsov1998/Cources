using MediatR;

namespace Courses.Application.Features.RefreshCourses;

public record RefreshCoursesQuery(int Year) : IRequest;
