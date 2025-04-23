using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FloralWisdom.Data;
using FloralWisdom.Models.Entities;

namespace FloralWisdom.WebApp.Controllers
{
    public class UserPlantsController : Controller
    {
        private readonly FloralWisdomDbContext _context;

        public UserPlantsController(FloralWisdomDbContext context)
        {
            _context = context;
        }

        // GET: UserPlants
        public async Task<IActionResult> Index()
        {
            var floralWisdomDbContext = _context.UserPlants.Include(u => u.Plant).Include(u => u.User);
            return View(await floralWisdomDbContext.ToListAsync());
        }

        // GET: UserPlants/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlant = await _context.UserPlants
                .Include(u => u.Plant)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userPlant == null)
            {
                return NotFound();
            }

            return View(userPlant);
        }

        // GET: UserPlants/Create
        public IActionResult Create()
        {
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserPlants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,PlantId")] UserPlant userPlant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userPlant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id", userPlant.PlantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userPlant.UserId);
            return View(userPlant);
        }

        // GET: UserPlants/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlant = await _context.UserPlants.FindAsync(id);
            if (userPlant == null)
            {
                return NotFound();
            }
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id", userPlant.PlantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userPlant.UserId);
            return View(userPlant);
        }

        // POST: UserPlants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,PlantId")] UserPlant userPlant)
        {
            if (id != userPlant.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPlant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPlantExists(userPlant.UserId))
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
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id", userPlant.PlantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userPlant.UserId);
            return View(userPlant);
        }

        // GET: UserPlants/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlant = await _context.UserPlants
                .Include(u => u.Plant)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userPlant == null)
            {
                return NotFound();
            }

            return View(userPlant);
        }

        // POST: UserPlants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userPlant = await _context.UserPlants.FindAsync(id);
            if (userPlant != null)
            {
                _context.UserPlants.Remove(userPlant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPlantExists(string id)
        {
            return _context.UserPlants.Any(e => e.UserId == id);
        }
    }
}
