using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class ContactPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
