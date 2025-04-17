using Microsoft.AspNetCore.Mvc.Filters;
using SD.Mini.ZooManagement.Api.Extensions;
using SD.Mini.ZooManagement.Api.FilterAttributes.Utils;
using SD.Mini.ZooManagement.Application.Exceptions.Application;

namespace SD.Mini.ZooManagement.Api.FilterAttributes;

public class ExceptionFilter: IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        string callId = context.HttpContext.TraceIdentifier;

        _logger.LogApplicationException(
            exception: context.Exception,
            curTime: DateTime.Now,
            callId: callId
        );
        
        switch (context.Exception)
        {
            // TODO: Add
            case ApplicationValidationException ex:
                ErrorRequestHandler.HandleBadRequest(context, ex);
                break;
            
            default:
                ErrorRequestHandler.HandleInternalError(context);
                break;
        }
    }
}