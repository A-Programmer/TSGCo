using Project.BlazorApp.Contracts;

namespace Project.BlazorApp.Services;

public class LoggerService : ILoggerService
{
    public async Task Log(string message)
    {
        Console.WriteLine($"\n\n\n\n{message}\n\n\n\n");
    }
}