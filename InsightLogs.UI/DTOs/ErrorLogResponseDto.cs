namespace InsightLogs.UI.DTOs
{
    public class ErrorLogResponse
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<ErrorLogDto> Data { get; set; } = new();
    }
}
