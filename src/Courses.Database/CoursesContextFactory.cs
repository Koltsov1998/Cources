using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Courses.Database;

public class CoursesContextFactory : IDesignTimeDbContextFactory<CoursesDbContext>
{
    public CoursesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CoursesDbContext>();
        optionsBuilder.UseNpgsql(
            "Server=127.0.0.1;Port=5432;Database=courses;User Id=postgres;Password=postgres;");

        return new CoursesDbContext(optionsBuilder.Options);
    }
}