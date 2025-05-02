using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FloralWisdom.Data;
using FloralWisdom.Models.Entities;

namespace FloralWisdom.WebApp.Controllers
{
    public class CareRemindersController : Controller
    {
        private readonly FloralWisdomDbContext _context;

        public CareRemindersController(FloralWisdomDbContext context)
        {
            _context = context;
        }

        // GET: CareReminders
        public async Task<IActionResult> Index()
        {
            var floralWisdomDbContext = _context.CareReminders.Include(c => c.Plant);
            return View(await floralWisdomDbContext.ToListAsync());
        }

        // GET: CareReminders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careReminder = await _context.CareReminders
                .Include(c => c.Plant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (careReminder == null)
            {
                return NotFound();
            }

            return View(careReminder);
        }

        // GET: CareReminders/Create
        public IActionResult Create()
        {
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id");
            return View();
        }

        // POST: CareReminders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Remindertype,NextDueDate,PlantId")] CareReminder careReminder)
        {
            if (careReminder != null)
            {
                _context.Add(careReminder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id", careReminder.PlantId);
            return View(careReminder);
        }

        // GET: CareReminders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careReminder = await _context.CareReminders.FindAsync(id);
            if (careReminder == null)
            {
                return NotFound();
            }
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id", careReminder.PlantId);
            return View(careReminder);
        }

        // POST: CareReminders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Remindertype,NextDueDate,PlantId")] CareReminder careReminder)
        {
            if (id != careReminder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(careReminder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareReminderExists(careReminder.Id))
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
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id", careReminder.PlantId);
            return View(careReminder);
        }

        // GET: CareReminders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careReminder = await _context.CareReminders
                .Include(c => c.Plant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (careReminder == null)
            {
                return NotFound();
            }

            return View(careReminder);
        }

        // POST: CareReminders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var careReminder = await _context.CareReminders.FindAsync(id);
            if (careReminder != null)
            {
                _context.CareReminders.Remove(careReminder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CareReminderExists(string id)
        {
            return _context.CareReminders.Any(e => e.Id == id);
        }
    }
}
