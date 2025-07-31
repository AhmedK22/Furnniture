using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureManagement.Presentation.Controllers
{
    public class BaseAPIController : ControllerBase
    {

        protected IActionResult Problem(int code, string title, string message)
        {


            return Problem(detail: message, title: title, statusCode: code);
        }

    }
}
