namespace InsightLogs.Domain.Entities;

public class ErrorLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Service { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string StackTrace { get; set; } = string.Empty;
    public string Severity { get; set; } = "Not defined"; // Low, Medium, High
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool Resolved { get; set; } = false;
    public string ErrorMetadataJson { get; set; } = string.Empty;
    public string ReportedBy { get; set; } = "System";
}
