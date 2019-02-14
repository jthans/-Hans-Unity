using Hans.Logging.Enums;
using Hans.Logging.Interfaces;
using Hans.Logging.Models;
using System.ComponentModel.Composition;
using UnityEngine;

namespace Assets.Scripts.Core.Logging
{
    /// <summary>
    ///  Log Exporter that's used to export logging to the Unity Engine during debugging.  This is a very powerful
    ///     exporter, as we'll be able to choose what logs to show at any given time.
    /// </summary>
    [Export(typeof(ILogExporter))]
    public class UnityLogger : ILogExporter
    {
        /// <summary>
        ///  Exports a log given to it in the Unity Editor.
        /// </summary>
        /// <param name="logToExport">The log to output.</param>
        public void ExportLog(Log logToExport)
        {
#if UNITY_EDITOR
            switch (logToExport.Level)
            {
                case LogLevel.Warning:
                    Debug.LogWarning(logToExport.ToString());
                    break;
                case LogLevel.Error:
                case LogLevel.Fatal:
                    Debug.LogError(logToExport.ToString());
                    break;
                case LogLevel.Debug:
                case LogLevel.Information:
                default:
                    Debug.Log(logToExport.ToString());
                    break;
            }
#endif
        }
    }
}
