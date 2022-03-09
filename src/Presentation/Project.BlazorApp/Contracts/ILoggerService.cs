namespace Project.BlazorApp.Contracts;

public interface ILoggerService
{
    Task Log(string message);
}