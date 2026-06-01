using ConferenceBookingApp.Data;
using ConferenceBookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBookingApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PageContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PageContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PageContents.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var content = await _context.PageContents.FindAsync(id);
            if (content == null) return NotFound();

            return View(content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Key,Value")] PageContent pageContent)
        {
            if (id != pageContent.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageContentExists(pageContent.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pageContent);
        }

        private bool PageContentExists(int id)
        {
            return _context.PageContents.Any(e => e.Id == id);
        }
    }
}
