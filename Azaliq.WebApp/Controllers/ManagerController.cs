using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    public class ManagerController : BaseController
    {
        public IActionResult Index()
        {
            return View("_Layout");
        }
    }
}
