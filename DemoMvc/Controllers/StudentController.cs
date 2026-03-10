using Microsoft.AspNetCore.Mvc;
using DemoMvc.Data;
using DemoMvc.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
namespace DemoMvc.Controllers
{
    public class StudentController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public IActionResult Index()
        {
            // Lấy danh sách sinh viên từ cơ sở dữ liệu
            var listStudents = _context.Students.ToList();
            //truyen danh sách sinh viên vào view
            return View(listStudents);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            _context.Students.Add(std);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(string id)
        {
            //Tim sinh viên theo mã sinh viên
            var std = await _context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student std)
        {
            _context.Entry(std).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // 1. Action hiển thị trang xác nhận xóa (HttpGet)
public async Task<IActionResult> Delete(string id)
{
    if (id == null) return NotFound();

    var std = await _context.Students.FindAsync(id);
    if (std == null) return NotFound();

    return View(std);
}

// 2. Action thực hiện việc xóa sau khi nhấn nút xác nhận (HttpPost)
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(string id)
{
    var std = await _context.Students.FindAsync(id);
    if (std != null)
    {
        _context.Students.Remove(std); // Lệnh xóa bản ghi
        await _context.SaveChangesAsync(); // Lưu thay đổi xuống database
    }
    return RedirectToAction(nameof(Index));
}
    }
}