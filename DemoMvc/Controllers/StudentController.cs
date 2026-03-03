using Microsoft.AspNetCore.Mvc;
using DemoMvc.Models.Entities;
using DemoMvc.Data; // Thêm dòng này để Controller thấy được DbContext
namespace DemoMvc.Controllers
{
    public class StudentController : Controller
{
    // 1. Khai báo DbContext
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(Student std)
    {
        if (ModelState.IsValid)
        {
            // 2. Thêm vào bảng Students
            _context.Add(std);
            
            // 3. LƯU THAY ĐỔI XUỐNG FILE .DB (Cực kỳ quan trọng)
            await _context.SaveChangesAsync();

            ViewBag.ThongBao = "Đã lưu thành công sinh viên: " + std.FullName + " - Mã: " + std.StudentCode;
        }
        return View();
    }
}
}