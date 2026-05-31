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
    public class ProfessorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Professors.ToListAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professors = await _context.Professors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professors == null)
            {
                return NotFound();
            }

            return View(professors);
        }

        // GET: Professors/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AcademicTitle,FirstName,LastName")] Professors professors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professors);
        }

        // GET: Professors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professors = await _context.Professors.FindAsync(id);
            if (professors == null)
            {
                return NotFound();
            }
            return View(professors);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AcademicTitle,FirstName,LastName")] Professors professors)
        {
            if (id != professors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorsExists(professors.Id))
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
            return View(professors);
        }

        // GET: Professors/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professors = await _context.Professors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professors == null)
            {
                return NotFound();
            }

            return View(professors);
        }

        // POST: Professors/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professors = await _context.Professors.FindAsync(id);
            if (professors != null)
            {
                _context.Professors.Remove(professors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorsExists(int id)
        {
            return _context.Professors.Any(e => e.Id == id);
        }
    }
}
