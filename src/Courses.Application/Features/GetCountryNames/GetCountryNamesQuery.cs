using System.Collections.Immutable;
using MediatR;

namespace Courses.Application.Features.GetCountryNames;

public record GetCountryNamesQuery() : IRequest<ImmutableArray<string>>;
