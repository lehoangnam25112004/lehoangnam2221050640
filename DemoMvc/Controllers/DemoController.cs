using Microsoft.AspNetCore.Mvc;
namespace DemoMvc.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            //su dung viewbag de gui du lieu tu controller ve view
            ViewBag.FullName = "Welcome to Demo MVC!";
            return View();
        }
        public IActionResult Demo()
        {
            return View();
        }
    }
}