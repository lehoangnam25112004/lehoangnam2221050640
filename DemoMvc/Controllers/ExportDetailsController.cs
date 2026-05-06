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
    public class ExportDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExportDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExportDetails.Include(e => e.ExportTicket).Include(e => e.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExportDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportDetail = await _context.ExportDetails
                .Include(e => e.ExportTicket)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportDetail == null)
            {
                return NotFound();
            }

            return View(exportDetail);
        }

        // GET: ExportDetails/Create
        public IActionResult Create()
        {
            ViewData["ExportTicketId"] = new SelectList(_context.ExportTickets, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: ExportDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExportTicketId,ProductId,Quantity,UnitPrice")] ExportDetail exportDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exportDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExportTicketId"] = new SelectList(_context.ExportTickets, "Id", "Id", exportDetail.ExportTicketId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", exportDetail.ProductId);
            return View(exportDetail);
        }

        // GET: ExportDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportDetail = await _context.ExportDetails.FindAsync(id);
            if (exportDetail == null)
            {
                return NotFound();
            }
            ViewData["ExportTicketId"] = new SelectList(_context.ExportTickets, "Id", "Id", exportDetail.ExportTicketId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", exportDetail.ProductId);
            return View(exportDetail);
        }

        // POST: ExportDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExportTicketId,ProductId,Quantity,UnitPrice")] ExportDetail exportDetail)
        {
            if (id != exportDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exportDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportDetailExists(exportDetail.Id))
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
            ViewData["ExportTicketId"] = new SelectList(_context.ExportTickets, "Id", "Id", exportDetail.ExportTicketId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", exportDetail.ProductId);
            return View(exportDetail);
        }

        // GET: ExportDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportDetail = await _context.ExportDetails
                .Include(e => e.ExportTicket)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportDetail == null)
            {
                return NotFound();
            }

            return View(exportDetail);
        }

        // POST: ExportDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exportDetail = await _context.ExportDetails.FindAsync(id);
            if (exportDetail != null)
            {
                _context.ExportDetails.Remove(exportDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExportDetailExists(int id)
        {
            return _context.ExportDetails.Any(e => e.Id == id);
        }
    }
}
