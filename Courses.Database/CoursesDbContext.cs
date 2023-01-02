using Courses.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.Database;

public class CoursesDbContext : DbContext
{
    public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options){}

    public DbSet<CourseDbo> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var courseEntity = modelBuilder
            .Entity<CourseDbo>();
        
        courseEntity
            .HasKey(entity => new { entity.Date, CountryTextCode = entity.CurrencyName });
        courseEntity
            .Property(entity => entity.Date)
            .HasColumnType("date");
    }
}