using Microsoft.AspNetCore.Mvc;

namespace Finance_manager.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
