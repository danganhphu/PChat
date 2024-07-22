using PChat.Domain.Abstractions;

namespace PChat.Domain.Entities;

public sealed class ErrorLog : BaseEntity
{
    public string ErrorId { get; set; } = Guid.NewGuid().ToString(); // Unique identifier
    public string? ErrorMessage { get; set; }
    public string? StackTrace { get; set; }
    public string? RequestPath { get; set; }
    public string? RequestMethod { get; set; }
    public DateTime Timestamp { get; set; }
}
