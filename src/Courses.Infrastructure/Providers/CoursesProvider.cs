using System.Collections.Immutable;
using System.Globalization;
using Courses.Core;
using Courses.Core.Providers;
using Courses.Infrastructure.Clients;

namespace Courses.Infrastructure.Providers;

public class CoursesProvider : ICoursesProvider
{
  private readonly CoursesHttpClient _client;

  public CoursesProvider(CoursesHttpClient client)
  {
    _client = client;
  }

  public async Task<ImmutableArray<Course>> GetCourses(int year, CancellationToken cancellationToken)
  {
    var response = await _client.Client.GetAsync(
      $"/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year={year}",
      cancellationToken);

    var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

    return ParseCoursesResponse(responseContent).ToImmutableArray();
  }

  private IEnumerable<Course> ParseCoursesResponse(string response)
  {
    var strings = new Queue<string>(response.Split("\n"));

    var headerString = strings.Dequeue();

    var currencies = headerString
      .Split("|")
      .Skip(1)
      .Select(currency =>
      (
        currencyName: currency.Split(" ")[1],
        mupltiplier: decimal.Parse(currency.Split(" ")[0], CultureInfo.InvariantCulture)))
      .ToArray();

    while (strings.TryDequeue(out var courseString))
    {
      if (courseString.Length == 0) yield break;

      var courseValues = courseString.Split("|");

      var date = DateTime.ParseExact(courseValues[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);

      for (var i = 1; i < courseValues.Length; i++)
      {
        var courseValue = decimal.Parse(courseValues[i], CultureInfo.InvariantCulture);
        var currency = currencies[i - 1];

        yield return new Course(
          date,
          currency.currencyName,
          currency.mupltiplier * courseValue);
      }
    }
  }
}
