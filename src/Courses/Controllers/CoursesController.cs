using System.Collections.Immutable;
using System.ComponentModel;
using Courses.Application.Features.GetCountryNames;
using Courses.Application.Features.GetCourses;
using Courses.Application.Features.RefreshCourses;
using Courses.Controllers.Mappings;
using Courses.Controllers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Controllers;

[ApiController]
[Route("courses")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<GetCoursesResponse> GetCourses(
        DateTime? dateFromUtc,
        DateTime? dateToUtc,
        string currency,
        int pageNumber = 0,
        int pageSize = 25,
        CancellationToken cancellationToken = default)
    {
        var command = new GetCoursesQuery(
            dateFromUtc,
            dateToUtc,
            currency,
            pageNumber,
            pageSize);

        var result = await _mediator.Send(command, cancellationToken);

        return result.MapToResponse();
    }

    [HttpGet("countries/names")]
    public async Task<ImmutableArray<string>> GetCountryNames(
      CancellationToken cancellationToken)
    {
      var command = new GetCountryNamesQuery();

      var result = await _mediator.Send(command, cancellationToken);

      return result;
    }

    [HttpPut("refresh")]
    public async Task<IActionResult> RefreshCourses(
        [FromBody][DefaultValue(2015)] int year,
        CancellationToken cancellationToken)
    {
        if (year < 2000 || year > DateTime.Now.Year)
        {
            return BadRequest($"Year must be between 2000 and {DateTime.Now.Year}");
        }

        var command = new RefreshCoursesQuery(year);

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}
