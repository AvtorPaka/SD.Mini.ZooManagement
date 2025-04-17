using Microsoft.AspNetCore.Mvc;
using SD.Mini.ZooManagement.Api.Contracts.Responses;

namespace SD.Mini.ZooManagement.Api.FilterAttributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class ErrorResponseTypeAttribute : ProducesResponseTypeAttribute
{
    public ErrorResponseTypeAttribute(int statusCode) : base(typeof(ErrorResponse), statusCode)
    {
    }
}