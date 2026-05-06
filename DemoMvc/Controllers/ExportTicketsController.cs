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
    public class ExportTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExportTickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExportTickets.ToListAsync());
        }

        // GET: ExportTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportTicket = await _context.ExportTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportTicket == null)
            {
                return NotFound();
            }

            return View(exportTicket);
        }

        // GET: ExportTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExportTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExportDate,ReceiverName,TotalAmount")] ExportTicket exportTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exportTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exportTicket);
        }

        // GET: ExportTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportTicket = await _context.ExportTickets.FindAsync(id);
            if (exportTicket == null)
            {
                return NotFound();
            }
            return View(exportTicket);
        }

        // POST: ExportTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExportDate,ReceiverName,TotalAmount")] ExportTicket exportTicket)
        {
            if (id != exportTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exportTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportTicketExists(exportTicket.Id))
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
            return View(exportTicket);
        }

        // GET: ExportTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportTicket = await _context.ExportTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportTicket == null)
            {
                return NotFound();
            }

            return View(exportTicket);
        }

        // POST: ExportTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exportTicket = await _context.ExportTickets.FindAsync(id);
            if (exportTicket != null)
            {
                _context.ExportTickets.Remove(exportTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExportTicketExists(int id)
        {
            return _context.ExportTickets.Any(e => e.Id == id);
        }
    }
}
