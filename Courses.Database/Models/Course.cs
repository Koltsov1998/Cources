namespace Courses.Database.Models;

public record Course(
    DateTime Date,
    string CountryTextCode,
    decimal Value);