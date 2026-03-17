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

        // 1. Hiển thị danh sách
        public IActionResult Index()
        {
            var listStudents = _context.Students.ToList();
            return View(listStudents);
        }

        // 2. Trang Thêm mới (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 3. Xử lý Thêm mới (POST)
                [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                // 1. Kiểm tra xem trong Database có ai dùng mã StudentCode này chưa
                var isDuplicate = _context.Students.Any(s => s.StudentCode == std.StudentCode);

                if (isDuplicate)
                {
                    // 2. Nếu đã tồn tại, chuyển hướng ngay sang trang NotFound
                    // Lưu ý: "NotFound" ở đây là tên của View bạn vừa tạo ở Bước 1
                    return View("NotFound"); 
                }

                // 3. Nếu không trùng thì mới lưu bình thường
                _context.Students.Add(std);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
    
    return View(std);
}

        // 4. Trang Chỉnh sửa (GET)
                public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return View("NotFound");

            var std = await _context.Students.FindAsync(id);
            
            // Nếu không tìm thấy sinh viên với id này
            if (std == null)
            {
                return View("NotFound");
            }
            
            return View(std);
        }

        // 5. Xử lý Chỉnh sửa (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(std).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return View(std);
        }

        // 6. Trang Xác nhận xóa (GET)
                public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return View("NotFound");

            var std = await _context.Students.FindAsync(id);
            
            // Nếu sinh viên đã bị ai đó xóa trước đó rồi
            if (std == null)
            {
                return View("NotFound");
            }
            
            return View(std);
        }
        // 7. Xử lý Xóa (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await _context.Students.FindAsync(id);
            if (std != null)
            {
                _context.Students.Remove(std);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

