using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using DemoMvc.Data; 
using DemoMvc.Helpers;

namespace DemoMvc.Controllers
{
    public class BaseController<T> : Controller where T : class, new()
    {
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

    [HttpPost]
    public virtual async Task<IActionResult> Import(IFormFile file)
    {
        if (file == null || file.Length == 0) return RedirectToAction("Index");

        // Dùng ExcelHelper để biến file thành danh sách đối tượng
        var data = ExcelHelper.ToList<T>(file);

        foreach (var item in data)
        {
            // Thêm lệnh Try-Catch này để nếu 1 dòng trùng thì nó bỏ qua, chạy tiếp dòng sau
            try 
            {
                _context.Set<T>().Add(item);
                await _context.SaveChangesAsync();
            }
            catch {
                // Dòng này lỗi (do trùng mã) thì bỏ qua dòng này
                _context.Entry(item).State = EntityState.Detached; 
                continue; 
            }
        }
        return RedirectToAction("Index");
    }
        }
}