using Microsoft.AspNetCore.Mvc;

namespace Item_Code_management_System.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
