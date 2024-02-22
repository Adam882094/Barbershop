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
    public class BarbersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarbersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Barbers
        public async Task<IActionResult> Index()
        {
            var orderedBarbers = await _context.Barbers
                                             .OrderBy(b => b.BarberName)
                                             .ToListAsync();
            return View(orderedBarbers);
        }

        // GET: Barbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Barbers == null)
            {
                return NotFound();
            }

            var barber = await _context.Barbers
                .FirstOrDefaultAsync(m => m.BarberId == id);
            if (barber == null)
            {
                return NotFound();
            }

            return View(barber);
        }

        // GET: Barbers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Barbers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BarberId,BarberName")] Barber barber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(barber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine(errors);
            }
            return View(barber);
        }

        // GET: Barbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Barbers == null)
            {
                return NotFound();
            }

            var barber = await _context.Barbers.FindAsync(id);
            if (barber == null)
            {
                return NotFound();
            }
            return View(barber);
        }

        // POST: Barbers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BarberId,BarberName")] Barber barber)
        {
            if (id != barber.BarberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarberExists(barber.BarberId))
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
            return View(barber);
        }

        // GET: Barbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Barbers == null)
            {
                return NotFound();
            }

            var barber = await _context.Barbers
                .FirstOrDefaultAsync(m => m.BarberId == id);
            if (barber == null)
            {
                return NotFound();
            }

            return View(barber);
        }

        // POST: Barbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Barbers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Barbers'  is null.");
            }
            var barber = await _context.Barbers.FindAsync(id);
            if (barber != null)
            {
                _context.Barbers.Remove(barber);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarberExists(int id)
        {
          return (_context.Barbers?.Any(e => e.BarberId == id)).GetValueOrDefault();
        }
    }
}
