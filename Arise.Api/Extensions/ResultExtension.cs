
using Arise.Application.Common.Enums;
using Arise.Application.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace Arise.Api.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result)
        {
            if (result.IsSuccess) return new OkObjectResult(result);

            var statusCode = MapErrorTypeToStatusCode(result.ErrorType);

            if (result.ValidationErrors is not null)
            {
                return new BadRequestObjectResult(new ValidationProblemDetails(result.ValidationErrors)
                {
                    Status = statusCode,
                    Title = result.Error
                });
            }

            return new ObjectResult(new ProblemDetails { Title = result.Error, Status = statusCode })
            {
                StatusCode = statusCode
            };
        }

        public static IActionResult ToActionResult<T>(this Result<T> result) =>
            result.IsSuccess ? new OkObjectResult(result) : ((Result)result).ToActionResult();

        private static int MapErrorTypeToStatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
