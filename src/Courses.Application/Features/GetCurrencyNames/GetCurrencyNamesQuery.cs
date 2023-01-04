using System.Collections.Immutable;
using MediatR;

namespace Courses.Application.Features.GetCurrencyNames;

public record GetCurrencyNamesQuery() : IRequest<ImmutableArray<string>>;
