namespace SD.Mini.ZooManagement.Api.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(
        LogLevel.Information,
        EventId = 2000,
        Message = "[{CallId}] [{CurTime}] Start executing call. Endpoint: {EndpointRoute}"
    )]
    public static partial void LogRequestStart(this ILogger logger,
        DateTime curTime,
        string callId,
        string endpointRoute);


    [LoggerMessage(
        LogLevel.Information,
        EventId = 2001,
        Message = "[{CallId}] [{CurTime}] Ended executing call. Endpoint: {EndpointRoute}"
    )]
    public static partial void LogRequestEnd(this ILogger logger,
        DateTime curTime,
        string callId,
        string endpointRoute);
    
    [LoggerMessage(
        Level = LogLevel.Error,
        EventId = 4001,
        Message = "[{CallId}] [{CurTime}] Exception occured during request processing."
    )]
    public static partial void LogApplicationException(this ILogger logger,
        Exception exception,
        string callId,
        DateTime curTime);
    
    [LoggerMessage(
        LogLevel.Debug,
        EventId = 1000,
        Message = "[{CallId}] [{CurTime}] Request headers:\n{Headers}"
    )]
    public static partial void LogRequestHeaders(this ILogger logger,
        DateTime curTime,
        string callId,
        string headers);
}