using System.Runtime.InteropServices;

namespace Avalara.AvaTax.Adapter.LogService
{
    /// <summary>
    /// LogLevel enum
    /// </summary>
    [Guid("85C9C335-E376-4be4-A830-D86CE8EA5FC0")]
    [ComVisible(true)]
    public enum LogLevel
    {
        /// <summary>
        /// None
        /// </summary>
        NONE = -1, 
        
        /// <summary>
        /// Debug
        /// </summary>
        DEBUG, 
        
        /// <summary>
        /// Info
        /// </summary>
        INFO, 
        
        /// <summary>
        /// Warning
        /// </summary>
        WARNING, 
        
        /// <summary>
        /// Error
        /// </summary>
        ERROR, 
        
        /// <summary>
        /// Fatal
        /// </summary>
        FATAL
    };

    /// <summary>
    /// LogSvc
    /// </summary>
    [Guid("8E8B9308-7E9F-4036-A567-747F804AA207")]
    [ComVisible(true)]
    public class LogSvc
    {
        /// <summary>
        /// Logs the client message to Adapter log file
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        [DispId(1)]
        public void LogMessage(string clientName, LogLevel messageType, string message)
        {
            switch (messageType)
            {
                case LogLevel.DEBUG:
                    avaLogger.Debug(clientName + "," + message);
                    break;
                case LogLevel.INFO:
                    avaLogger.Information(clientName + "," + message);
                    break;
                case LogLevel.WARNING:
                    avaLogger.Warning(clientName + "," + message);
                    break;
                case LogLevel.ERROR:
                    avaLogger.Error(clientName + "," + message);
                    break;
                case LogLevel.FATAL:
                    avaLogger.Fail(clientName + "," + message);
                    break;
            }
        }

        private readonly AvaLogger avaLogger = AvaLogger.GetLogger();
    }
}
