using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.Slack;

namespace Core;

public static class LogFactory
{
    private const string Source = "XCab";

    private const string SlackWebHookUrl =
        "https://hooks.slack.com/services/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";

    private const string SlackChannelName = "#xcab";
#if !DEBUG
    private const string LogFileFullPath = @"D:\Logs\XCab-Service\Live\XCabService.log";
#endif
#if DEBUG
    private const string LogFileFullPath = @"C:\Temp\XCabService.log";
#endif

    private const string TextLogFormat =
        @"{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] <{SourceContext}>: {Message:lj}{NewLine}{Exception}";

    public static ILogger<T> GetFileLogger<T>()
    {
        var serilogLogger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.File(LogFileFullPath,
                outputTemplate: TextLogFormat,
                rollingInterval: RollingInterval.Day
            )
            .CreateLogger();

        return new SerilogLoggerFactory(serilogLogger).CreateLogger<T>();
    }

    public static ILogger<T> GetSlackLogger<T>()
    {
        var serilogLogger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.Slack(SlackWebHookUrl)
            .CreateLogger();

        return new SerilogLoggerFactory(serilogLogger).CreateLogger<T>();
    }

    public static ILogger<T> GetCombinedLogger<T>()
    {
        var serilogLogger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.Slack(SlackWebHookUrl)
            .WriteTo.File(
                LogFileFullPath,
                outputTemplate: TextLogFormat,
                rollingInterval: RollingInterval.Day
            )
            .WriteTo.Console()
            .CreateLogger();

        return new SerilogLoggerFactory(serilogLogger).CreateLogger<T>();
    }
}