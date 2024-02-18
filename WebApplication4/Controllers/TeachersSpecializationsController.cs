using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;

namespace WebApplication4.Controllers
{
    public class TeachersSpecializationsController : Controller
    {
        private readonly DbcoursesContext _context;

        public TeachersSpecializationsController(DbcoursesContext context)
        {
            _context = context;
        }

        // GET: TeachersSpecializations
        public async Task<IActionResult> Index()
        {
            var dbcoursesContext = _context.TeachersSpecializations.Include(t => t.Specialization).Include(t => t.Teachers);
            return View(await dbcoursesContext.ToListAsync());
        }

        // GET: TeachersSpecializations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachersSpecialization = await _context.TeachersSpecializations
                .Include(t => t.Specialization)
                .Include(t => t.Teachers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teachersSpecialization == null)
            {
                return NotFound();
            }

            return View(teachersSpecialization);
        }

        // GET: TeachersSpecializations/Create
        public IActionResult Create()
        {
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "Id", "Id");
            ViewData["TeachersId"] = new SelectList(_context.Teachers, "Id", "Id");
            return View();
        }

        // POST: TeachersSpecializations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeachersId,SpecializationId")] TeachersSpecialization teachersSpecialization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teachersSpecialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "Id", "Id", teachersSpecialization.SpecializationId);
            ViewData["TeachersId"] = new SelectList(_context.Teachers, "Id", "Id", teachersSpecialization.TeachersId);
            return View(teachersSpecialization);
        }

        // GET: TeachersSpecializations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachersSpecialization = await _context.TeachersSpecializations.FindAsync(id);
            if (teachersSpecialization == null)
            {
                return NotFound();
            }
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "Id", "Id", teachersSpecialization.SpecializationId);
            ViewData["TeachersId"] = new SelectList(_context.Teachers, "Id", "Id", teachersSpecialization.TeachersId);
            return View(teachersSpecialization);
        }

        // POST: TeachersSpecializations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeachersId,SpecializationId")] TeachersSpecialization teachersSpecialization)
        {
            if (id != teachersSpecialization.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teachersSpecialization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeachersSpecializationExists(teachersSpecialization.Id))
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
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "Id", "Id", teachersSpecialization.SpecializationId);
            ViewData["TeachersId"] = new SelectList(_context.Teachers, "Id", "Id", teachersSpecialization.TeachersId);
            return View(teachersSpecialization);
        }

        // GET: TeachersSpecializations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachersSpecialization = await _context.TeachersSpecializations
                .Include(t => t.Specialization)
                .Include(t => t.Teachers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teachersSpecialization == null)
            {
                return NotFound();
            }

            return View(teachersSpecialization);
        }

        // POST: TeachersSpecializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teachersSpecialization = await _context.TeachersSpecializations.FindAsync(id);
            if (teachersSpecialization != null)
            {
                _context.TeachersSpecializations.Remove(teachersSpecialization);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeachersSpecializationExists(int id)
        {
            return _context.TeachersSpecializations.Any(e => e.Id == id);
        }
    }
}
