using System.Diagnostics;

namespace InsightLogs.Application.Helpers
{
    public static class ExceptionInfoExtractor
    {
        public static ErrorMetadata Extract(Exception ex)
        {
            var st = new StackTrace(ex, true);
            foreach (var frame in st.GetFrames())
            {
                var method = frame.GetMethod();
                var declaringType = method?.DeclaringType;

                if (declaringType == null || declaringType.Namespace?.StartsWith("System") == true)
                    continue;

                return new ErrorMetadata
                {
                    Assembly = declaringType.Assembly.GetName().Name ?? "Unknown",
                    Namespace = declaringType.Namespace ?? "Unknown",
                    Class = declaringType.Name,
                    Method = method.Name,
                    File = frame.GetFileName() ?? "N/A",
                    Line = frame.GetFileLineNumber()
                };
            }

            return new ErrorMetadata
            {
                Assembly = "Unknown",
                Namespace = "Unknown",
                Class = "Unknown",
                Method = "Unknown",
                File = "N/A",
                Line = 0
            };
        }
    }

    public class ErrorMetadata
    {
        public string Assembly { get; set; } = string.Empty;
        public string Namespace { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
        public int Line { get; set; }
    }
}
