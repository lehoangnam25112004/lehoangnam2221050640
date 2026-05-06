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
    public class ImportDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ImportDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ImportDetails.Include(i => i.ImportTicket).Include(i => i.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ImportDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importDetail = await _context.ImportDetails
                .Include(i => i.ImportTicket)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importDetail == null)
            {
                return NotFound();
            }

            return View(importDetail);
        }

        // GET: ImportDetails/Create
        public IActionResult Create()
        {
            ViewData["ImportTicketId"] = new SelectList(_context.ImportTickets, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: ImportDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImportTicketId,ProductId,Quantity,UnitPrice")] ImportDetail importDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImportTicketId"] = new SelectList(_context.ImportTickets, "Id", "Id", importDetail.ImportTicketId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", importDetail.ProductId);
            return View(importDetail);
        }

        // GET: ImportDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importDetail = await _context.ImportDetails.FindAsync(id);
            if (importDetail == null)
            {
                return NotFound();
            }
            ViewData["ImportTicketId"] = new SelectList(_context.ImportTickets, "Id", "Id", importDetail.ImportTicketId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", importDetail.ProductId);
            return View(importDetail);
        }

        // POST: ImportDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImportTicketId,ProductId,Quantity,UnitPrice")] ImportDetail importDetail)
        {
            if (id != importDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportDetailExists(importDetail.Id))
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
            ViewData["ImportTicketId"] = new SelectList(_context.ImportTickets, "Id", "Id", importDetail.ImportTicketId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", importDetail.ProductId);
            return View(importDetail);
        }

        // GET: ImportDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importDetail = await _context.ImportDetails
                .Include(i => i.ImportTicket)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importDetail == null)
            {
                return NotFound();
            }

            return View(importDetail);
        }

        // POST: ImportDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importDetail = await _context.ImportDetails.FindAsync(id);
            if (importDetail != null)
            {
                _context.ImportDetails.Remove(importDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportDetailExists(int id)
        {
            return _context.ImportDetails.Any(e => e.Id == id);
        }
    }
}
