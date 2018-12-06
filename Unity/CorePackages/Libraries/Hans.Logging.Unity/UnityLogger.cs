using Hans.Logging.Interfaces;
using System.ComponentModel.Composition;
using Hans.Logging.Models;
using UnityEngine;

namespace Hans.Logging.Unity
{
    /// <summary>
    ///  Log Exporter that will export log messages out to the Unity engine, to display in the Console/any other aggregators that read from Unity's log feed.
    /// </summary>
    [Export(typeof(ILogExporter))]
    public class UnityLogger : ILogExporter
    {
        /// <summary>
        ///  Exports the log message to the Unity console.
        /// </summary>
        /// <param name="logToExport">The log message to send out to the console.</param>
        public void ExportLog(Log logToExport)
        {
            switch (logToExport.Level)
            {
                case Enums.LogLevel.Warning:
                    Debug.LogWarning(logToExport.ToString());
                    break;
                case Enums.LogLevel.Error:
                case Enums.LogLevel.Fatal:
                    Debug.LogError(logToExport.ToString());
                    break;
                case Enums.LogLevel.Debug:
                case Enums.LogLevel.Information:
                default:
                    Debug.Log(logToExport.ToString());
                    break;
            }
        }
    }
}
