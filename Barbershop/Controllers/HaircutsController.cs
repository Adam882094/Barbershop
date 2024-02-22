using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Barbershop.Data;
using Barbershop.Models;
using Microsoft.AspNetCore.Authorization;

namespace Barbershop.Controllers
{
    //Adminstartor role only
    [Authorize(Roles = "Administrator")]
    public class HaircutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HaircutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Haircuts
        public async Task<IActionResult> Index()
        {
              return _context.Haircut != null ? 
                          View(await _context.Haircut.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Haircut'  is null.");
        }

        // GET: Haircuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Haircut == null)
            {
                return NotFound();
            }

            var haircut = await _context.Haircut
                .FirstOrDefaultAsync(m => m.HaircutId == id);
            if (haircut == null)
            {
                return NotFound();
            }

            return View(haircut);
        }

        // GET: Haircuts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Haircuts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HaircutId,Name,Price")] Haircut haircut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(haircut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(haircut);
        }

        // GET: Haircuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Haircut == null)
            {
                return NotFound();
            }

            var haircut = await _context.Haircut.FindAsync(id);
            if (haircut == null)
            {
                return NotFound();
            }
            return View(haircut);
        }

        // POST: Haircuts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaircutId,Name,Price")] Haircut haircut)
        {
            if (id != haircut.HaircutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(haircut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HaircutExists(haircut.HaircutId))
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
            return View(haircut);
        }

        // GET: Haircuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Haircut == null)
            {
                return NotFound();
            }

            var haircut = await _context.Haircut
                .FirstOrDefaultAsync(m => m.HaircutId == id);
            if (haircut == null)
            {
                return NotFound();
            }

            return View(haircut);
        }

        // POST: Haircuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Haircut == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Haircut'  is null.");
            }
            var haircut = await _context.Haircut.FindAsync(id);
            if (haircut != null)
            {
                _context.Haircut.Remove(haircut);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HaircutExists(int id)
        {
          return (_context.Haircut?.Any(e => e.HaircutId == id)).GetValueOrDefault();
        }
    }
}
