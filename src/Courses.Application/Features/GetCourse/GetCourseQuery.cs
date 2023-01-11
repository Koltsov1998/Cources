using MediatR;

namespace Courses.Application.Features.GetCourse;

public record GetCourseQuery(string CurrencyName, DateTime DateUtc) : IRequest<GetCourseResult?>;
