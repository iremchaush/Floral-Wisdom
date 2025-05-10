using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FloralWisdom.Data;
using FloralWisdom.Models.Entities;

namespace FloralWisdom.WebApp.Controllers
{
    public class DiseaseReportsController : Controller
    {
        private readonly FloralWisdomDbContext _context;

        public DiseaseReportsController(FloralWisdomDbContext context)
        {
            _context = context;
        }

        // GET: DiseaseReports
        public async Task<IActionResult> Index()
        {
            var floralWisdomDbContext = _context.DiseaseReports.Include(d => d.Plant);
            return View(await floralWisdomDbContext.ToListAsync());
        }

        // GET: DiseaseReports/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseReport = await _context.DiseaseReports
                .Include(d => d.Plant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diseaseReport == null)
            {
                return NotFound();
            }

            return View(diseaseReport);
        }

        // GET: DiseaseReports/Create
        public IActionResult Create()
        {
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id");
            return View();
        }

        // POST: DiseaseReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Diagnosis,RecommendedTreatment,PlantId")] DiseaseReport diseaseReport)
        {
            if (diseaseReport!=null)
            {
                diseaseReport.Id = Guid.NewGuid().ToString();
                _context.Add(diseaseReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id", diseaseReport.PlantId);
            return View(diseaseReport);
        }

        // GET: DiseaseReports/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseReport = await _context.DiseaseReports.FindAsync(id);
            if (diseaseReport == null)
            {
                return NotFound();
            }
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id", diseaseReport.PlantId);
            return View(diseaseReport);
        }

        // POST: DiseaseReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Diagnosis,RecommendedTreatment,PlantId")] DiseaseReport diseaseReport)
        {
            if (id != diseaseReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diseaseReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseReportExists(diseaseReport.Id))
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
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Id", diseaseReport.PlantId);
            return View(diseaseReport);
        }

        // GET: DiseaseReports/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseReport = await _context.DiseaseReports
                .Include(d => d.Plant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diseaseReport == null)
            {
                return NotFound();
            }

            return View(diseaseReport);
        }

        // POST: DiseaseReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var diseaseReport = await _context.DiseaseReports.FindAsync(id);
            if (diseaseReport != null)
            {
                _context.DiseaseReports.Remove(diseaseReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseaseReportExists(string id)
        {
            return _context.DiseaseReports.Any(e => e.Id == id);
        }
    }
}
