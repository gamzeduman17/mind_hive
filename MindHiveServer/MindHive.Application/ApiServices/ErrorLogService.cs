using MindHive.Application.ApiServiceInterfaces;
using MindHive.Domain.Entities;
using MindHive.Domain.Repositories;

namespace MindHive.Application.ApiServices;

public class ErrorLogService : IErrorLogService
{
    private readonly IErrorLogRepository _errorLogRepository;
    public ErrorLogService(IErrorLogRepository errorLogRepository)
    {
        _errorLogRepository = errorLogRepository;
    }

    public async Task LogAsync(string serviceName, string message, string? stackTrace = null)
    {
        var log = new ErrorLog
        {
            ServiceName = serviceName,
            ExceptionMessage = message,
            StackTrace = stackTrace
        };
        await _errorLogRepository.AddAsync(log);
        await _errorLogRepository.SaveChangesAsync();
    }
}