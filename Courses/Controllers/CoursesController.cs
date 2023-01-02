using System.ComponentModel;
using Courses.Application.Features.RefreshCourses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cources.Controllers;

[ApiController]
[Route("courses")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
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