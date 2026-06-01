using ConferenceBookingApp.Data;
using ConferenceBookingApp.Models;
using ConferenceBookingApp.Services;
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


    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public BookingsController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Bookings
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.ConferenceRoom)
                .Include(b => b.Professor)
                .ToListAsync();

            return View(bookings);
        }

        // GET: Bookings/Details/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.ConferenceRoom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        // GET: Bookings/Create
        [Authorize(Roles = "Admin, User")]
        public IActionResult Create()
        {
            var roomsList = _context.ConferenceRooms
                .Select(r => new { Id = r.Id, DisplayText = "Sala nr " + r.Nnumber })
                .ToList();
            ViewData["ConferenceRoomId"] = new SelectList(roomsList, "Id", "DisplayText");
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "FullName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,MeetingPurpose,ConferenceRoomId,ProfessorId")] Bookings booking)
        {

            ModelState.Remove("UserId");
            ModelState.Remove("Professor");
            ModelState.Remove("ConferenceRoom");

            TimeSpan openTime = new TimeSpan(8, 0, 0);
            TimeSpan closeTime = new TimeSpan(22, 15, 0);

            // Czy data zakończenia jest po dacie rozpoczęcia
            if (booking.EndDate <= booking.StartDate)
            {
                ModelState.AddModelError("EndDate", "Data zakończenia spotkania musi być późniejsza niż data rozpoczęcia!");
            }

            // Czy rezerwacja nie dotyczy przeszłości
            if (booking.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("StartDate", "Nie można rezerwować sali na czas, który już minął!");
            }
            //nie można zarezerwowac sali na kilka dni
            if (booking.StartDate.Date != booking.EndDate.Date)
            {
                ModelState.AddModelError("EndDate", "Rezerwacja musi rozpoczynać i kończyć się tego samego dnia! Nie można rezerwować sali na wiele dni.");
            }

            if (booking.StartDate.TimeOfDay < openTime || booking.StartDate.TimeOfDay > closeTime)
            {
                ModelState.AddModelError("StartDate", "Sala może być rezerwowana wyłącznie w godzinach od 08:00 do 22:15!");
            }

            if (booking.EndDate.TimeOfDay < openTime || booking.EndDate.TimeOfDay > closeTime)
            {
                ModelState.AddModelError("EndDate", "Godzina zakończenia spotkania musi mieścić się w przedziale od 08:00 do 22:15!");
            }

            bool isRoomOccupied = _context.Bookings.Any(b =>
                b.ConferenceRoomId == booking.ConferenceRoomId &&
                booking.StartDate < b.EndDate &&
                booking.EndDate > b.StartDate
            );

            if (isRoomOccupied)
            {
                ModelState.AddModelError(string.Empty, "Ta sala jest już zarezerwowana w wybranym przedziale czasowym!");
            }

            var professor = await _context.Professors.FindAsync(booking.ProfessorId);
            var room = await _context.ConferenceRooms.FindAsync(booking.ConferenceRoomId);

            if (professor != null && !string.IsNullOrEmpty(professor.Email))
            {
                string temat = "Potwierdzenie rezerwacji sali konferencyjnej";

                string trescHtml = $@"
                <h2>Dzień dobry, {professor.AcademicTitle} {professor.LastName}!</h2>
                <p>W systemie została dla Państwa utworzona nowa rezerwacja.</p>
                <hr/>
                <ul>
                    <li><strong>Sala:</strong> Pokój nr {room?.Nnumber} (Piętro {room?.Floor})</li>
                    <li><strong>Cel spotkania:</strong> {booking.MeetingPurpose}</li>
                    <li><strong>Od:</strong> {booking.StartDate.ToString("dd.MM.yyyy HH:mm")}</li>
                    <li><strong>Do:</strong> {booking.EndDate.ToString("dd.MM.yyyy HH:mm")}</li>
                </ul>
                <hr/>
                <p>Wiadomość wygenerowana automatycznie. Prosimy na nią nie odpowiadać.</p>";
                await _emailSender.SendEmailAsync(professor.Email, temat, trescHtml);
            }

            if (ModelState.IsValid)
            {

                booking.UserId = "BrakUzytkownika";

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            var roomsList = _context.ConferenceRooms.Select(r => new { Id = r.Id, DisplayText = "Sala nr " + r.Nnumber }).ToList();
            ViewData["ConferenceRoomId"] = new SelectList(roomsList, "Id", "DisplayText", booking.ConferenceRoomId);
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "FullName", booking.ProfessorId);

            return View(booking);
        }
        // GET: Bookings/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRooms, "Id", "Floor", bookings.ConferenceRoomId);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,MeetingPurpose,ConferenceRoomId,UserId")] Bookings bookings)
        {
            if (id != bookings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.Id))
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
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRooms, "Id", "Floor", bookings.ConferenceRoomId);
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.ConferenceRoom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }


        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }

        // GET: Bookings/GetCalendarData
        [Authorize(Roles = "Admin,User")]
        public async Task<JsonResult> GetCalendarData()
        {
            var bookings = await _context.Bookings
                .Include(b => b.ConferenceRoom)
                .Include(b => b.Professor)
                .ToListAsync();

            // Mapujemy nasze rezerwacje na format akceptowany przez FullCalendar
            var calendarEvents = bookings.Select(b => new
            {
                id = b.Id,
                title = $"Sala {b.ConferenceRoom.Nnumber} - {b.Professor.FullName} ({b.MeetingPurpose})",
                start = b.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = b.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                color = b.Id % 2 == 0 ? "#2c3e50" : "#16a085"
            });

            return Json(calendarEvents);
        }

        // GET: Bookings/Calendar
        [Authorize(Roles = "Admin,User")]
        public IActionResult Calendar()
        {
            return View();
        }
    }
}
