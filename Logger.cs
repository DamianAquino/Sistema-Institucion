using System.Text;

namespace API_Institucion
{
    public class Logger
    {
        private static readonly object _lock = new();
        private static readonly string _logDir = "logs";
        private static readonly string _logFile =
            Path.Combine(_logDir, $"api-{DateTime.UtcNow:yyyy-MM-dd}.log");

        public static void Log(string level, string message)
        {
            try
            {
                if (!Directory.Exists(_logDir))
                    Directory.CreateDirectory(_logDir);

                var logLine = $"{DateTime.UtcNow:O} [{level}] {message}";

                lock (_lock)
                {
                    File.AppendAllText(_logFile, logLine + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch
            {
                // Nunca romper la app por un log
            }
        }
        public static void Info(string message) =>
            Log("INFO", message);

        public static void Warning(string message) =>
            Log("WARN", message);

        public static void Error(string message) =>
            Log("ERROR", message);
    }

}
