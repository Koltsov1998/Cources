namespace Courses.Infrastructure.Clients;

public class CoursesHttpClient
{
    public HttpClient Client { get; private set; }
    
    public CoursesHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
    }
}