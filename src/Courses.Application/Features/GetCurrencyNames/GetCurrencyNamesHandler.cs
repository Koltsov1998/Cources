using System.Collections.Immutable;
using Courses.Application.Repositories;
using MediatR;

namespace Courses.Application.Features.GetCurrencyNames;

public class GetCountryNamesHandler : IRequestHandler<GetCurrencyNamesQuery, ImmutableArray<string>>
{
  private readonly ICourseRepository _courseRepository;

  public GetCountryNamesHandler(ICourseRepository courseRepository)
  {
    _courseRepository = courseRepository;
  }

  public async Task<ImmutableArray<string>> Handle(
    GetCurrencyNamesQuery request,
    CancellationToken cancellationToken)
  {
    return await _courseRepository.GetAllCurrencyNames(cancellationToken);
  }
}
