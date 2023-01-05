namespace Courses.Infrastructure.Clients;

public class CoursesHttpClient
{
  public CoursesHttpClient(HttpClient httpClient)
  {
    Client = httpClient;
  }

  public HttpClient Client { get; }
}
