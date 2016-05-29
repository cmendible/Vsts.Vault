namespace Vsts.Vault.Logging
{
    using System;
    using System.ComponentModel.Composition;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vsts.Vault.Logging.ILogger" />
    [Export(typeof(ILogger))]
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// The layout
        /// </summary>
        private const string layout = "{0} - {1} - {2} - {3}";

        /// <summary>
        /// The type
        /// </summary>
        private string type = typeof(VaultService).Namespace;

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(object message)
        {
            this.WriteToConsole("DEBUG", message);
        }

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void DebugFormat(string format, params object[] args)
        {
            this.WriteToConsole("DEBUG", format, args);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(object message)
        {
            this.WriteToConsole("ERROR", message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Error(object message, Exception exception)
        {
            this.WriteToConsole("ERROR", message, exception);
        }

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void ErrorFormat(string format, params object[] args)
        {
            this.WriteToConsole("ERROR", format, args);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(object message)
        {
            this.WriteToConsole("FATAL", message);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Fatal(object message, Exception exception)
        {
            this.WriteToConsole("FATAL", message, exception);
        }

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void FatalFormat(string format, params object[] args)
        {
            this.WriteToConsole("FATAL", format, args);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(object message)
        {
            this.WriteToConsole("INFO", message);
        }

        /// <summary>
        /// Informations the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void InfoFormat(string format, params object[] args)
        {
            this.WriteToConsole("INFO", format, args);
        }

        /// <summary>
        /// Currents the date time.
        /// </summary>
        /// <returns></returns>
        private static string CurrentDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
        }

        /// <summary>
        /// Writes to console.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        private void WriteToConsole(string level, object message)
        {
            Console.WriteLine(string.Format(layout, CurrentDateTime(), level, this.type, message));
        }

        /// <summary>
        /// Writes to console.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        private void WriteToConsole(string level, object message, Exception exception)
        {
            Console.WriteLine(string.Format(layout, CurrentDateTime(), level, this.type, string.Format("{0} Exception: {1}", message, exception.ToString())));
        }

        /// <summary>
        /// Writes to console.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        private void WriteToConsole(string level, string format, params object[] args)
        {
            Console.WriteLine(string.Format(layout, CurrentDateTime(), level, this.type, string.Format(format, args)));
        }
    }
}