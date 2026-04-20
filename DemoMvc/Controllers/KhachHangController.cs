using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMvc.Data;
using DemoMvc.Models.Entities;

namespace DemoMvc.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KhachHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.KhachHangs.ToListAsync());
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKH == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: KhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKH,TenKH,DienThoai")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        // GET: KhachHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKH,TenKH,DienThoai")] KhachHang khachHang)
        {
            if (id != khachHang.MaKH)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.MaKH))
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
            return View(khachHang);
        }

        // GET: KhachHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKH == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHangs.Any(e => e.MaKH == id);
        }
        // GET: KhachHang/LichSuMuaHang/5
    // GET: KhachHang/LichSuMuaHang/5
    public async Task<IActionResult> LichSuMuaHang(int? id)
{
    if (id == null) return NotFound();

    // SỬA DÒNG NÀY: Dùng 'var' và thêm '!' hoặc khai báo rõ 'KhachHang?'
    var khachHang = await _context.KhachHangs
        .Include(k => k.DonHangs!)
            .ThenInclude(d => d.ChiTietDonHangs!)
                .ThenInclude(ct => ct.SanPham)
        .FirstOrDefaultAsync(m => m.MaKH == id);

    // Kiểm tra null ngay lập tức - Đây là chốt chặn quan trọng nhất
    if (khachHang == null) return NotFound();

    // Khởi tạo ViewModel
    var viewModel = new DemoMvc.Models.ViewModels.KhachHangDetailsVM() 
    {
        // Thêm '!' sau khachHang để khẳng định nó chắc chắn không null
        TenKH = khachHang!.TenKH ?? "N/A", 
        DanhSachDon = khachHang.DonHangs!.Select(d => new DemoMvc.Models.ViewModels.DonInfo()
        {
            MaDH = d.MaDH,
            NgayDat = d.NgayDat,
            TongTien = d.ChiTietDonHangs!.Sum(ct => ct.SoLuong * (ct.SanPham?.Gia ?? 0)),
            TenSanPhams = d.ChiTietDonHangs!.Select(ct => ct.SanPham?.TenSP ?? "N/A").ToList()
        }).ToList()
    };

    return View(viewModel);
}
    }
}
