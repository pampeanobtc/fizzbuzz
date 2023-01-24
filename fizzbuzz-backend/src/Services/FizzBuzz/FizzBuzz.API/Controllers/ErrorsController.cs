using FizzBuzz.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.API.Controllers;

[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("error")]
    public 
        ErrorResponse Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context.Error;
        var code = 500;

        if (exception is ArgumentException) 
            code = 400; // Bad Request

        Response.StatusCode = code; // You can use HttpStatusCode enum instead

        return new ErrorResponse(exception); // Your error model
    }
}
