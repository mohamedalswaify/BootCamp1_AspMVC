using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Models;

namespace BootCamp1_AspMVC.Controllers
{
    public class SupliersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupliersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Supliers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Supliers.ToListAsync());
        }

        // GET: Supliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suplier = await _context.Supliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suplier == null)
            {
                return NotFound();
            }

            return View(suplier);
        }

        // GET: Supliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone")] Suplier suplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suplier);
        }

        // GET: Supliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suplier = await _context.Supliers.FindAsync(id);
            if (suplier == null)
            {
                return NotFound();
            }
            return View(suplier);
        }

        // POST: Supliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone")] Suplier suplier)
        {
            if (id != suplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuplierExists(suplier.Id))
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
            return View(suplier);
        }

        // GET: Supliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suplier = await _context.Supliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suplier == null)
            {
                return NotFound();
            }

            return View(suplier);
        }

        // POST: Supliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suplier = await _context.Supliers.FindAsync(id);
            if (suplier != null)
            {
                _context.Supliers.Remove(suplier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuplierExists(int id)
        {
            return _context.Supliers.Any(e => e.Id == id);
        }
    }
}
