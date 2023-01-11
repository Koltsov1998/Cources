using MediatR;

namespace Courses.Application.Features.GetCourses;

public record GetCoursesQuery(
  DateTime? DateFromUtc,
  DateTime? DateToUtc,
  string CurrencyName,
  int PageNumber,
  int PageSize) : IRequest<GetCoursesResult>;
