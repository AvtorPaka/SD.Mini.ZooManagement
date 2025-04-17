using Microsoft.AspNetCore.Mvc.Filters;
using SD.Mini.ZooManagement.Api.Extensions;
using SD.Mini.ZooManagement.Api.FilterAttributes.Utils;
using SD.Mini.ZooManagement.Application.Exceptions.Application;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Animals;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Enclosures;

namespace SD.Mini.ZooManagement.Api.FilterAttributes;

public class ExceptionFilter : IExceptionFilter
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
            case AnimalTransferValidationException ex:
                ErrorRequestHandler.HandleTransferValidation(context, ex);
                break;
            
            case EnclosureCapacityConflictException ex:
                ErrorRequestHandler.HandleEnclosureCapacityConflict(context, ex);
                break;
            
            case EnclosureNotFoundException ex:
                ErrorRequestHandler.HandleEnclosureNotFound(context, ex);
                break;

            case AnimalHealDuplicateException ex:
                ErrorRequestHandler.HandleAnimalHealConflict(context, ex);
                break;

            case AnimalNotFoundException ex:
                ErrorRequestHandler.HandleAnimalNotFound(context, ex);
                break;

            case ApplicationValidationException ex:
                ErrorRequestHandler.HandleBadRequest(context, ex);
                break;

            default:
                ErrorRequestHandler.HandleInternalError(context);
                break;
        }
    }
}