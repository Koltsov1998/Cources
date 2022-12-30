using MediatR;

namespace Courses.Application.Features.RefreshCourses;

public class RefreshCoursesHandler : IRequestHandler<RefreshCoursesQuery>
{
    public Task<Unit> Handle(RefreshCoursesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}