using ConferenceBookingApp.Data;
using ConferenceBookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceBookingApp.Controllers
{
    public class ConferenceRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConferenceRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConferenceRooms
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ConferenceRooms.ToListAsync());
        }

        // GET: ConferenceRooms/Details/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceRooms = await _context.ConferenceRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conferenceRooms == null)
            {
                return NotFound();
            }

            return View(conferenceRooms);
        }

        // GET: ConferenceRooms/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConferenceRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nnumber,Floor,IsAvailable")] ConferenceRooms conferenceRooms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conferenceRooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conferenceRooms);
        }



        // GET: ConferenceRooms/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceRooms = await _context.ConferenceRooms.FindAsync(id);
            if (conferenceRooms == null)
            {
                return NotFound();
            }
            return View(conferenceRooms);
        }

        // POST: ConferenceRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nnumber,Floor,IsAvailable")] ConferenceRooms conferenceRooms)
        {
            if (id != conferenceRooms.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conferenceRooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConferenceRoomsExists(conferenceRooms.Id))
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
            return View(conferenceRooms);
        }

        // GET: ConferenceRooms/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceRooms = await _context.ConferenceRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conferenceRooms == null)
            {
                return NotFound();
            }

            return View(conferenceRooms);
        }

        // POST: ConferenceRooms/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conferenceRooms = await _context.ConferenceRooms.FindAsync(id);
            if (conferenceRooms != null)
            {
                _context.ConferenceRooms.Remove(conferenceRooms);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConferenceRoomsExists(int id)
        {
            return _context.ConferenceRooms.Any(e => e.Id == id);
        }

        // GET: ConferenceRooms/AvailableRooms
        [Authorize(Roles = "Admin,User")]
        public IActionResult AvailableRooms(DateTime? searchDate, TimeSpan? startTime, TimeSpan? endTime)
        {
            if (!searchDate.HasValue || !startTime.HasValue || !endTime.HasValue)
            {
                return View(new List<ConferenceRooms>());
            }

            // Łączymy datę z godzinami rozpoczęcia i zakończenia
            DateTime startFull = searchDate.Value.Date.Add(startTime.Value);
            DateTime endFull = searchDate.Value.Date.Add(endTime.Value);

            // Wyciągamy ID sal, które w tym czasie mają jakąs rezerwację
            var occupiedRoomIds = _context.Bookings
                .Where(b => startFull < b.EndDate && endFull > b.StartDate)
                .Select(b => b.ConferenceRoomId)
                .ToList();

            // Filtrujemy sale: bierzemy te, których ID nie ma na liście zajętych
            var availableRooms = _context.ConferenceRooms
                .Where(r => !occupiedRoomIds.Contains(r.Id))
                .ToList();

            ViewBag.SearchDate = searchDate.Value.ToString("yyyy-MM-dd");
            ViewBag.StartTime = startTime.Value.ToString(@"hh\:mm");
            ViewBag.EndTime = endTime.Value.ToString(@"hh\:mm");
            ViewBag.Searched = true;

            return View(availableRooms);
        }
    }
}
