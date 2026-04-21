using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMvc.Data;
using DemoMvc.Models.Entities;
using DemoMvc.Models.ViewModels;
using OfficeOpenXml;
using System.IO;

namespace DemoMvc.Controllers
{
    // public class StudentController : Controller
    public class StudentController : BaseController<Student>
    {
        // private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context): base(context)
        {
            // _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index(string searchString)
        {
            // Nạp sẵn bảng Class để lấy tên lớp
            var studentsQuery = _context.Students
                                        .Include(s => s.Class)
                                        .AsQueryable();

            // Chức năng tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                studentsQuery = studentsQuery.Where(s => s.FullName.Contains(searchString));
            }

            // Chuyển sang ViewModel an toàn (tránh lỗi NullReferenceException)
            var result = await studentsQuery
                .Select(s => new StudentVM
                {
                    StudentCode = s.StudentCode,
                    FullName = s.FullName,
                    Age = s.Age,
                    ClassName = s.Class != null ? s.Class.ClassName : "Chưa có lớp"
                })
                .ToListAsync();

            ViewData["CurrentFilter"] = searchString;
            return View(result);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class) // Phải có Include thì View mới gọi được model.Class.ClassName
                .FirstOrDefaultAsync(m => m.StudentCode == id);
                
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            // Trải danh sách Lớp học ra DropdownList
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentCode,FullName,Age,ClassId")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (StudentExists(student.StudentCode))
                {
                    ModelState.AddModelError("StudentCode", "Mã sinh viên đã tồn tại");
                    return View(student);
                }
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId","ClassName", student.ClassId);
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            // Load lại DropdownList với giá trị ClassId hiện tại của sinh viên
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", student.ClassId);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentCode,FullName,ClassId")] Student student)
        {
            if (id != student.StudentCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", student.ClassId);
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class) // Include để trang xác nhận xóa hiện được tên lớp
                .FirstOrDefaultAsync(m => m.StudentCode == id);
                
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentCode == id);
        }

    // [HttpGet]
    // public IActionResult Import()
    // {
    //     return View();
    // }

    //     [HttpPost]
    // public async Task<IActionResult> Import(IFormFile file)
    // {
    //     if (file == null || file.Length == 0) return BadRequest("Vui lòng chọn file.");

    //     // Xác nhận bản quyền cá nhân cho Nam
    //     ExcelPackage.License.SetNonCommercialPersonal("Nam");

    //     using (var stream = new MemoryStream())
    //     {
    //         await file.CopyToAsync(stream);
    //         using (var package = new ExcelPackage(stream))
    //         {
    //             ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
    //             int rowCount = worksheet.Dimension.Rows;

    //             for (int row = 2; row <= rowCount; row++)
    //             {
    //                 var studentCode = worksheet.Cells[row, 1].Value?.ToString();
                    
    //                 // Kiểm tra xem mã sinh viên đã tồn tại chưa để tránh lỗi trùng khóa chính (Key)
    //                 if (!string.IsNullOrEmpty(studentCode) && !_context.Students.Any(s => s.StudentCode == studentCode))
    //                 {
    //                     var std = new Student
    //                     {
    //                         StudentCode = studentCode,
    //                         FullName = worksheet.Cells[row, 2].Value?.ToString() ?? "N/A",
    //                         // Ép kiểu Age sang int? (nullable)
    //                         Age = int.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out int age) ? age : null,
    //                         // Gán ClassId (giả sử cột 4 trong Excel là ID của lớp)
    //                         ClassId = int.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out int classId) ? classId : 1 
    //                     };
    //                     _context.Students.Add(std);
    //                 }
    //             }
    //             await _context.SaveChangesAsync();
    //         }
    //     }
    //     return RedirectToAction(nameof(Index));
    // }
    // Trong StudentController.cs
public IActionResult Import()
{
    return View();
}

    }
}