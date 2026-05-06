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
    public class ImportTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ImportTickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImportTickets.ToListAsync());
        }

        // GET: ImportTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importTicket = await _context.ImportTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importTicket == null)
            {
                return NotFound();
            }

            return View(importTicket);
        }

        // GET: ImportTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImportTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImportDate,SupplierId,TotalAmount")] ImportTicket importTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(importTicket);
        }

        // GET: ImportTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importTicket = await _context.ImportTickets.FindAsync(id);
            if (importTicket == null)
            {
                return NotFound();
            }
            return View(importTicket);
        }

        // POST: ImportTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImportDate,SupplierId,TotalAmount")] ImportTicket importTicket)
        {
            if (id != importTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportTicketExists(importTicket.Id))
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
            return View(importTicket);
        }

        // GET: ImportTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importTicket = await _context.ImportTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importTicket == null)
            {
                return NotFound();
            }

            return View(importTicket);
        }

        // POST: ImportTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importTicket = await _context.ImportTickets.FindAsync(id);
            if (importTicket != null)
            {
                _context.ImportTickets.Remove(importTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportTicketExists(int id)
        {
            return _context.ImportTickets.Any(e => e.Id == id);
        }
    }
}
