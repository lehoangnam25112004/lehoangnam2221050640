using Microsoft.AspNetCore.Mvc;
using DemoMvc.Models.Entities;
namespace DemoMvc.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Student std)
        {
            ViewBag.ThongBao = "Xin chào: " + std.FullName + " - Mã sinh viên: " + std.StudentCode;
            return View();
        }
    }
}