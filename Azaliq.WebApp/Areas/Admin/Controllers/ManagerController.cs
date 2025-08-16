using Azaliq.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Areas.Admin.Controllers
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
