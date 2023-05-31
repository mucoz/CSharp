using System;
using System.IO;

public sealed class Logger : IDisposable
{
    private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());
    private readonly string logFolderPath = "Logs";
    private readonly string logFileName = $"Log_{DateTime.Now:yyyy_MM_dd}.txt";
    private readonly string startLogMessage = "==================== NEW PROCESS STARTED ====================";
    private readonly string finishLogMessage = "==================== END OF THE PROCESS ====================";
    private StreamWriter writer;

    private Logger()
    {
        InitializeLogFile();
    }

    public static Logger Instance => instance.Value;

    public static void Start()
    {
        Instance.Log(LogLevel.INFO, Instance.startLogMessage);
    }
    public static void Finish()
    {
        Instance.Log(LogLevel.INFO, Instance.finishLogMessage);
    }
    public static void Info(string message)
    {
        Instance.Log(LogLevel.INFO, message);
    }

    public static void Warn(string message)
    {
        Instance.Log(LogLevel.WARN, message);
    }

    public static void Error(string message)
    {
        Instance.Log(LogLevel.ERROR, message);
    }

    private void Log(LogLevel logLevel, string message)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][{logLevel}]: {message}";
        writer.WriteLine(logEntry);
        writer.Flush(); // Flush the writer to write the log entry immediately
    }

    private void InitializeLogFile()
    {
        //string logFileName = $"Log_{DateTime.Now:yyyy_MM_dd}.txt";
        string logFilePath = Path.Combine(logFolderPath, logFileName);

        Directory.CreateDirectory(logFolderPath);
        writer = File.AppendText(logFilePath);
    }

    public void Dispose()
    {
        writer.Dispose();
    }
}

public enum LogLevel
{
    INFO,
    WARN,
    ERROR
}
