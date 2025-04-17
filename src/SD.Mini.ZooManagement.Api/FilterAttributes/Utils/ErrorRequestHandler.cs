using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SD.Mini.ZooManagement.Api.Contracts.Responses;
using SD.Mini.ZooManagement.Application.Exceptions.Application;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Animals;

namespace SD.Mini.ZooManagement.Api.FilterAttributes.Utils;

internal static class ErrorRequestHandler
{
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