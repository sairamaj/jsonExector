using System.Diagnostics;

namespace JsonExecutor.Gui
{
    /// <summary>
    /// Class used to write trace statements for internal debugging.
    /// </summary>
    internal static class TraceLogger
    {
        /// <summary>
        /// Logs message at Debug level.
        /// </summary>
        /// <param name="msg">
        /// Message to be logged.
        /// </param>
        public static void Debug(string msg)
        {
            Trace.WriteLine($"[JsonExecutor.Gui-Debug] {msg}");
        }

        /// <summary>
        /// Logs message at Error level.
        /// </summary>
        /// <param name="msg">
        /// Message to be logged.
        /// </param>
        public static void Error(string msg)
        {
            Trace.WriteLine($"[JsonExecutor.Gui-Error] {msg}");
        }

        /// <summary>
        /// Logs message at Warning level.
        /// </summary>
        /// <param name="msg">
        /// Message to be logged.
        /// </param>
        public static void Warning(string msg)
        {
            Trace.WriteLine($"[JsonExecutor.Gui-Warning] {msg}");
        }
    }
}
