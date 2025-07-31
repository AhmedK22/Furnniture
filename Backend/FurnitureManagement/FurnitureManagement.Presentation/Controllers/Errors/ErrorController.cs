using FurnitureManagement.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureManagement.Presentation.Controllers.Errors
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public sealed class ErrorController : BaseAPIController
    {
        [HttpGet]
        public IActionResult Error()
        {

            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            // errors.Add(Errors.Unexpected.InternalServerError(exception.Message));
            var (resourceName, message, statusCode) = exception switch
            {
                ApplicationExceptionSystem appException => (appException.ResourceName, appException.ErrorDetails, Convert.ToInt32(appException.StatusCode)),
                _ => ("Server Error", "An Unexpected error occured", StatusCodes.Status500InternalServerError)
            };

            return Problem(code: statusCode, title: resourceName, message: message);
        }
    }
}
