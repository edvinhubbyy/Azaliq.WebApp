using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            ViewData["StatusCode"] = statusCode;

            return statusCode switch
            {
                403 => View("~/Views/Errors/Error403.cshtml"),
                404 => View("~/Views/Errors/Error404.cshtml"),
                500 => View("~/Views/Errors/Error500.cshtml"),
                _ => View("~/Views/Errors/Error.cshtml")
            };
        }
    }
}
