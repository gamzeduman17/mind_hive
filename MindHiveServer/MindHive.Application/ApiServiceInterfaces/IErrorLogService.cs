namespace MindHive.Application.ApiServiceInterfaces;

public interface IErrorLogService
{
    Task LogAsync(string serviceName, string message, string? stackTrace = null);
}