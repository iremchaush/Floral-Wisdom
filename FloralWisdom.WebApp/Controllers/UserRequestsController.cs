using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FloralWisdom.Data;
using FloralWisdom.Models.Entities;

namespace FloralWisdom.WebApp.Controllers
{
    public class UserRequestsController : Controller
    {
        private readonly FloralWisdomDbContext _context;

        public UserRequestsController(FloralWisdomDbContext context)
        {
            _context = context;
        }

        // GET: UserRequests
        public async Task<IActionResult> Index()
        {
            var floralWisdomDbContext = _context.UserRequests.Include(u => u.User);
            return View(await floralWisdomDbContext.ToListAsync());
        }

        // GET: UserRequests/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRequest = await _context.UserRequests
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRequest == null)
            {
                return NotFound();
            }

            return View(userRequest);
        }

        // GET: UserRequests/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,WorkHours,PlantType,Colour,UserId")] UserRequest userRequest)
        {
            if (userRequest!=null)
            {
                _context.Add(userRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userRequest.UserId);
            return View(userRequest);
        }

        // GET: UserRequests/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRequest = await _context.UserRequests.FindAsync(id);
            if (userRequest == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userRequest.UserId);
            return View(userRequest);
        }

        // POST: UserRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,WorkHours,PlantType,Colour,UserId")] UserRequest userRequest)
        {
            if (id != userRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRequestExists(userRequest.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userRequest.UserId);
            return View(userRequest);
        }

        // GET: UserRequests/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRequest = await _context.UserRequests
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRequest == null)
            {
                return NotFound();
            }

            return View(userRequest);
        }

        // POST: UserRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userRequest = await _context.UserRequests.FindAsync(id);
            if (userRequest != null)
            {
                _context.UserRequests.Remove(userRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRequestExists(string id)
        {
            return _context.UserRequests.Any(e => e.Id == id);
        }
    }
}
