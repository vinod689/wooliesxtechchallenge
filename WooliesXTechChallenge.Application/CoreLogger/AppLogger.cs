using Serilog;
using Serilog.Events;
using System;

namespace WooliesXTechChallenge.Application.CoreLogger
{
    public class AppLogger
    {
        private static readonly ILogger _errorLogger;

        static AppLogger()
        {
            _errorLogger = new LoggerConfiguration()
                .WriteTo.File(path: Environment.GetEnvironmentVariable("LOGFILE_ERROR"))
                .CreateLogger();
        }

        public static void WriteError(LogDetailModel logDetailModel)
        {
            if (logDetailModel.Exception != null)
            {
                logDetailModel.Location = logDetailModel.Location;
                logDetailModel.Message = GetMessageFromException(logDetailModel.Exception);
            }
            _errorLogger.Write(LogEventLevel.Error, messageTemplate: "{@LogDetailModel}", logDetailModel);
        }

        public static string GetMessageFromException(Exception Ex)
        {
            return Ex.Message.ToString();
        }
    }
}
