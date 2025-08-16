using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    [Area("Admin")]
    public class ManagerController : BaseController
    {
        public IActionResult Index()
        {
            return View("_Layout");
        }
    }
}
