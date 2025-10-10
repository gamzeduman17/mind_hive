namespace MindHive.Domain.Entities;

public class ErrorLog
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string ExceptionMessage { get; set; } = string.Empty;
    public string? StackTrace { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}