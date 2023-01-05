using MediatR;

namespace Courses.Application.Features.GetCourse;

public record GetCourseQuery(DateTime DateUtc) : IRequest<GetCourseResult?>;
