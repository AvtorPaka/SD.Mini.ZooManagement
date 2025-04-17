using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SD.Mini.ZooManagement.Api.Contracts.Responses;
using SD.Mini.ZooManagement.Application.Exceptions.Application;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Animals;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Enclosures;

namespace SD.Mini.ZooManagement.Api.FilterAttributes.Utils;

internal static class ErrorRequestHandler
{
    // Animal
    internal static void HandleAnimalHealConflict(ExceptionContext context, AnimalHealDuplicateException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.Conflict,
                Message: "Animal already healthy.",
                Exceptions: []
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.Conflict
        };

        context.Result = result;
    }

    internal static void HandleAnimalNotFound(ExceptionContext context, AnimalNotFoundException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.NotFound,
                Message: $"Animal with id: {ex.NotFoundException.InvalidId} couldn't be found.",
                Exceptions: [ex.NotFoundException.InvalidId.ToString()]
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.Result = result;
    }

    // Enclosure

    internal static void HandleEnclosureNotFound(ExceptionContext context, EnclosureNotFoundException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.NotFound,
                Message: $"Enclosure with id: {ex.NotFoundException.InvalidId} couldn't be found.",
                Exceptions: [ex.NotFoundException.InvalidId.ToString()]
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.Result = result;
    }

    internal static void HandleEnclosureCapacityConflict(ExceptionContext context,
        EnclosureCapacityConflictException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.Conflict,
                Message: ex.IsOverfill
                    ? "Enclosure capacity overflow"
                    : "Attempt to remove animal from empty enclosure",
                Exceptions: [ex.IsOverfill ? "Capacity overflow" : "Remove from empty attempt."]
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.Conflict
        };

        context.Result = result;
    }

    internal static void HandleTransferValidation(ExceptionContext context, AnimalTransferValidationException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.BadRequest,
                Message: ex.Message,
                Exceptions: []
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        context.Result = result;
    }


    //General

    internal static void HandleInternalError(ExceptionContext context)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.InternalServerError,
                Message: "Internal error. Check logs for detailed description.",
                Exceptions: ["Check logs for detailed description."]
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.Result = result;
    }

    internal static void HandleBadRequest(ExceptionContext context, ApplicationValidationException ex)
    {
        JsonResult result = new JsonResult(
            new ErrorResponse(
                StatusCode: HttpStatusCode.BadRequest,
                Message: "Invalid request parameters.",
                Exceptions: QueryUnvalidatedFields(ex.ValidationException)
            )
        )
        {
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        context.Result = result;
    }

    private static IEnumerable<string> QueryUnvalidatedFields(
        ValidationException exception)
    {
        foreach (ValidationFailure failure in exception.Errors)
        {
            yield return $">> Field: {failure.PropertyName}, Error: {failure.ErrorMessage}\n";
        }
    }
}