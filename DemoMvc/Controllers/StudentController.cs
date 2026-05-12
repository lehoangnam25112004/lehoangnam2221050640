using DemoMvc.Data;
using DemoMvc.Models.Entities;
using DemoMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoMvc.Controllers
{
    public class StudentController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult Index()
        {
            return View();
        }

        // =========================
        // GET LIST + PAGING
        // =========================
        public async Task<IActionResult> GetStudents(int page = 1, int pageSize = 10)
        {
            var query = _context.Students
                .Include(x => x.Class)
                .AsNoTracking()
                .OrderByDescending(x => x.StudentCode);

            var totalItems = await query.CountAsync();

            var students = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<Student>
            {
                Items = students,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return PartialView("StudentTable", result);
        }

        // =========================
        // CREATE
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ClassId = new SelectList(_context.Classes.ToList(), "ClassId", "ClassName");
            return PartialView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ClassId = new SelectList(_context.Classes.ToList(), "ClassId", "ClassName");
                return PartialView("Create", student);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // =========================
        // EDIT
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(string studentCode)
        {
            var student = await _context.Students
                .Include(x => x.Class)
                .FirstOrDefaultAsync(x => x.StudentCode == studentCode);

            if (student == null)
                return NotFound();

            ViewBag.ClassId = new SelectList(_context.Classes.ToList(), "ClassId", "ClassName");
            return PartialView("Edit", student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ClassId = new SelectList(_context.Classes.ToList(), "ClassId", "ClassName");
                return PartialView("Edit", student);
            }

            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(x => x.StudentCode == student.StudentCode);

            if (existingStudent == null)
                return NotFound();

            existingStudent.FullName = student.FullName;
            existingStudent.Age = student.Age;
            existingStudent.ClassId = student.ClassId;

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // =========================
        // DELETE
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string studentCode)
        {
            var student = await _context.Students
                .Include(x => x.Class)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StudentCode == studentCode);

            if (student == null)
                return NotFound();

            return PartialView("Delete", student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Student student)
        {
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(x => x.StudentCode == student.StudentCode);

            if (existingStudent == null)
                return Json(new { success = false });

            _context.Students.Remove(existingStudent);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}