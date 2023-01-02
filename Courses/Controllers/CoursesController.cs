using Courses.Application.Features.RefreshCourses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cources.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("refresh")]
    public async Task RefreshCourses(CancellationToken cancellationToken)
    {
        var command = new RefreshCoursesQuery();

        await _mediator.Send(command, cancellationToken);
    }
}