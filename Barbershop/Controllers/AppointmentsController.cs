﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Barbershop.Data;
using Barbershop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Barbershop.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var applicationDbContext = _context.Appointments.Include(a => a.Barber).Include(a => a.Haircut).Where(a => a.CustomerId == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Barber)
                .Include(a => a.Haircut)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "BarberName");
            ViewData["HaircutId"] = new SelectList(_context.Haircut, "HaircutId", "Name");
            ViewBag.BookedTimes = new List<DateTime>(); // Ensure this is always initialized
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentDateTime,BarberId,HaircutId,CustomerId")] Appointment appointment)
        {
            var user = await _userManager.GetUserAsync(User);
            appointment.CustomerId = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "BarberId", appointment.BarberId);
            ViewData["HaircutId"] = new SelectList(_context.Haircut, "HaircutId", "HaircutId", appointment.HaircutId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "BarberName", appointment.BarberId);
            ViewData["HaircutId"] = new SelectList(_context.Haircut, "HaircutId", "Name", appointment.HaircutId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,AppointmentDateTime,BarberId,HaircutId,CustomerId")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "BarberId", appointment.BarberId);
            ViewData["HaircutId"] = new SelectList(_context.Haircut, "HaircutId", "HaircutId", appointment.HaircutId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Barber)
                .Include(a => a.Haircut)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetAvailableTimes(DateTime date, int barberId)
        {
            var today = DateTime.Today;
            var now = DateTime.Now;
            // If the selected date is today, set the start time to the current time, otherwise to the start of the workday.
            var startTime = date.Date == today ? new DateTime(today.Year, today.Month, today.Day, now.Hour, now.Minute, 0).AddMinutes(40 - (now.Minute % 40)) : date.Date.AddHours(9);
            var endTime = date.Date.AddHours(17); // Assuming a 5 PM end

            // Get all the times that are already booked for that barber on the selected date
            var bookedTimes = await _context.Appointments
                .Where(a => a.BarberId == barberId && a.AppointmentDateTime.Date == date.Date)
                .Select(a => a.AppointmentDateTime)
                .ToListAsync();

            // Generate all possible timeslots for that day
            var timeSlots = Enumerable.Range(0, (int)(endTime - startTime).TotalMinutes / 40)
                .Select(i => startTime.AddMinutes(i * 40))
                .ToList();

            // Select the times that are not booked
            var availableTimes = timeSlots
                .Where(t => !bookedTimes.Contains(t))
                .Select(t => new { value = t.ToString("o"), text = t.ToString("HH:mm") })
                .ToList();

            return Json(availableTimes);
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
